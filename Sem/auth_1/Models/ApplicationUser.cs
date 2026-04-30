using Microsoft.AspNetCore.Identity;

namespace auth_1.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Avatar { get; set; } = string.Empty;
    public bool HidePersonalInformation { get; set; } = false;
}
