using Microsoft.EntityFrameworkCore;
using auth_1.Data;
using auth_1.Models;

namespace auth_1.Services;

public interface IFriendService
{
    Task<bool> SendFriendRequestAsync(string senderId, string receiverId);
    Task<bool> AcceptFriendRequestAsync(int requestId);
    Task<bool> RejectFriendRequestAsync(int requestId);
    Task<List<FriendRequest>> GetPendingRequestsAsync(string userId);
    Task<List<FriendRequest>> GetSentRequestsAsync(string userId);
    Task<List<ApplicationUser>> GetFriendsAsync(string userId);
    Task<List<ApplicationUser>> SearchUsersAsync(string currentUserId, string query);
    Task<bool> CancelFriendRequestAsync(int requestId);
}

public class FriendService : IFriendService
{
    private readonly ApplicationDbContext _context;

    public FriendService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SendFriendRequestAsync(string senderId, string receiverId)
    {
        var existingRequest = await _context.FriendRequests
            .AnyAsync(f => (f.SenderId == senderId && f.ReceiverId == receiverId) || 
                           (f.SenderId == receiverId && f.ReceiverId == senderId));

        if (existingRequest) return false;

        var request = new FriendRequest
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Status = FriendRequestStatus.Pending,
            Timestamp = DateTime.UtcNow
        };

        _context.FriendRequests.Add(request);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AcceptFriendRequestAsync(int requestId)
    {
        var request = await _context.FriendRequests.FindAsync(requestId);
        if (request == null) return false;

        request.Status = FriendRequestStatus.Accepted;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectFriendRequestAsync(int requestId)
    {
        var request = await _context.FriendRequests.FindAsync(requestId);
        if (request == null) return false;

        request.Status = FriendRequestStatus.Rejected;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CancelFriendRequestAsync(int requestId)
    {
        var request = await _context.FriendRequests.FindAsync(requestId);
        if (request == null) return false;

        _context.FriendRequests.Remove(request);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<FriendRequest>> GetPendingRequestsAsync(string userId)
    {
        return await _context.FriendRequests
            .Include(f => f.Sender)
            .Where(f => f.ReceiverId == userId && f.Status == FriendRequestStatus.Pending)
            .ToListAsync();
    }

    public async Task<List<FriendRequest>> GetSentRequestsAsync(string userId)
    {
        return await _context.FriendRequests
            .Include(f => f.Receiver)
            .Where(f => f.SenderId == userId && f.Status == FriendRequestStatus.Pending)
            .ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetFriendsAsync(string userId)
    {
        var friends = await _context.FriendRequests
            .Where(f => (f.SenderId == userId || f.ReceiverId == userId) && f.Status == FriendRequestStatus.Accepted)
            .Select(f => f.SenderId == userId ? f.Receiver : f.Sender)
            .ToListAsync();

        return friends!;
    }

    public async Task<List<ApplicationUser>> SearchUsersAsync(string currentUserId, string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<ApplicationUser>();

        var existingFriendIds = await _context.FriendRequests
            .Where(f => f.SenderId == currentUserId || f.ReceiverId == currentUserId)
            .Select(f => f.SenderId == currentUserId ? f.ReceiverId : f.SenderId)
            .ToListAsync();

        return await _context.Users
            .Where(u => u.Id != currentUserId && 
                        !existingFriendIds.Contains(u.Id) &&
                        u.UserName!.Contains(query))
            .ToListAsync();
    }
}
