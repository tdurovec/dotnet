using Microsoft.EntityFrameworkCore;
using auth_1.Data;
using auth_1.Models;

namespace auth_1.Services;

public interface IChatService
{
    Task<List<ChatMessage>> GetChatHistoryAsync(string user1Id, string user2Id);
    Task SaveMessageAsync(ChatMessage message);
    Task<List<ApplicationUser>> GetUsersAsync();
    Task MarkAsReadAsync(string currentUserId, string senderId);
    Task<Dictionary<string, int>> GetUnreadCountsAsync(string currentUserId);
}

public class ChatService : IChatService
{
    private readonly ApplicationDbContext _context;

    public ChatService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChatMessage>> GetChatHistoryAsync(string user1Id, string user2Id)
    {
        return await _context.ChatMessages
            .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                        (m.SenderId == user2Id && m.ReceiverId == user1Id))
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }

    public async Task SaveMessageAsync(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ApplicationUser>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task MarkAsReadAsync(string currentUserId, string senderId)
    {
        var unreadMessages = await _context.ChatMessages
            .Where(m => m.ReceiverId == currentUserId && m.SenderId == senderId && !m.IsRead)
            .ToListAsync();

        if (unreadMessages.Any())
        {
            foreach (var msg in unreadMessages)
            {
                msg.IsRead = true;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Dictionary<string, int>> GetUnreadCountsAsync(string currentUserId)
    {
        return await _context.ChatMessages
            .Where(m => m.ReceiverId == currentUserId && !m.IsRead)
            .GroupBy(m => m.SenderId)
            .Select(g => new { SenderId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.SenderId, x => x.Count);
    }
}
