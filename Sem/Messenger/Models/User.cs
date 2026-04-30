using Microsoft.AspNetCore.Identity;

namespace Messenger.Models;

public class User : IdentityUser
{
    public string Avatar { get; set; } = string.Empty;
}