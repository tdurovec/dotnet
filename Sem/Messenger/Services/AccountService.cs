using Microsoft.AspNetCore.Identity;
using Messenger.Schemas;
using Messenger.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Services;
public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterInput input);
    Task<SignInResult> LoginAsync(LoginInput input);
    Task<string> UploadAvatarAsync(IBrowserFile file);
}

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IWebHostEnvironment _environment;
    private readonly IChatService _chatService;

    public AccountService(
        UserManager<User> userManager, 
        SignInManager<User> signInManager,
        IChatService chatService,
        IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _chatService = chatService;
        _environment = environment;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterInput input)
    {
        var user = new User { UserName = input.UserName };
        user.Avatar = await GetDefaultAvatarAsBase64Async();
        
        return await _userManager.CreateAsync(user, input.Password);
    }

    public async Task<string> UploadAvatarAsync(IBrowserFile file)
    {
        var uploads = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
        if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var filePath = Path.Combine(uploads, fileName);

        using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5); // 5MB limit
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream);

        return $"/uploads/avatars/{fileName}";
    }

    public async Task<SignInResult> LoginAsync(LoginInput input)
    {
        return await _signInManager.PasswordSignInAsync(input.UserName, input.Password, input.RememberMe, lockoutOnFailure: false);
    }
    private string FormatBase64(string contentType, byte[] buffer) 
            => $"data:{contentType};base64,{Convert.ToBase64String(buffer)}";

    // Pre nahraný súbor z UI (Profile.razor)
    public async Task<string> ConvertToBase64Async(IBrowserFile file, int maxWidth = 250, int maxHeight = 250)
    {
        var resizedFile = await file.RequestImageFileAsync(file.ContentType, maxWidth, maxHeight);
        var buffer = new byte[resizedFile.Size];
        
        using var stream = resizedFile.OpenReadStream();
        await stream.ReadExactlyAsync(buffer);
        
        return FormatBase64(file.ContentType, buffer);
    }

    private async Task<string> GetDefaultAvatarAsBase64Async()
    {
        var path = Path.Combine(_environment.WebRootPath, "avatars", "default.png");
        
        if (!File.Exists(path)) return string.Empty;
        var bytes = await File.ReadAllBytesAsync("/avatars/default.png");
        
        // Pri default.png vieme, že je to image/png
        return FormatBase64("image/png", bytes);
    }

    // V UserService.cs alebo AccountService.cs
    public async Task<string> GenerateChatExportAsync(User user)
    {
        var allUsers = await _userManager.Users.ToListAsync();
        var export = new System.Text.StringBuilder();
        
        export.AppendLine($"Chat Export for {user.UserName}");
        export.AppendLine($"Generated: {DateTime.Now}");
        export.AppendLine(new string('-', 30));
        export.AppendLine();

        foreach (var friend in allUsers.Where(u => u.Id != user.Id))
        {
            // Tu voláš tvoj ChatService, ktorý musíš injectnúť do konštruktora tejto služby
            var messages = await _chatService.GetChatHistoryAsync(user.Id, friend.Id);
            
            if (messages != null && messages.Any())
            {
                export.AppendLine($"--- CONVERSATION WITH {friend.UserName} ---");
                foreach (var m in messages)
                {
                    var sender = m.SenderId == user.Id ? "Me" : friend.UserName;
                    export.AppendLine($"[{m.Timestamp:yyyy-MM-dd HH:mm}] {sender}: {m.Message}");
                }
                export.AppendLine();
            }
        }

        return export.ToString();
    }
}
