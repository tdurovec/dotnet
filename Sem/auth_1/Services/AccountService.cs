using Microsoft.AspNetCore.Identity;
using auth_1.Schemas.Account;
using auth_1.Models;

namespace auth_1.Services;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterInput input);
    Task<SignInResult> LoginAsync(LoginInput input);
    Task LogoutAsync();
    Task<string> UploadAvatarAsync(Microsoft.AspNetCore.Components.Forms.IBrowserFile file);
}

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IWebHostEnvironment _environment;

    public AccountService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _environment = environment;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterInput input)
    {
        var avatarUrl = "";
        if (input.AvatarFile != null)
        {
            avatarUrl = await UploadAvatarAsync(input.AvatarFile);
        }

        var user = new ApplicationUser 
        { 
            UserName = input.UserName,
            FirstName = input.FirstName,
            LastName = input.LastName,
            BirthDate = input.BirthDate,
            Avatar = avatarUrl
        };
        return await _userManager.CreateAsync(user, input.Password);
    }

    public async Task<string> UploadAvatarAsync(Microsoft.AspNetCore.Components.Forms.IBrowserFile file)
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

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
