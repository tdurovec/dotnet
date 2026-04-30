using System.ComponentModel.DataAnnotations;

namespace auth_1.Schemas.Account;

public class ProfileInput
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Birthdate")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "Hide Personal Information")]
    public bool HidePersonalInformation { get; set; }
}
