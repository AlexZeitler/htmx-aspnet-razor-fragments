using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcHtmx.Models;

public class Registration
{
  [Display(Name = "First name")]
  [Required]
  public string? FirstName { get; set; }
  [Required]
  [Display(Name = "Last name")]
  public string? LastName { get; set; }
  [Required]
  [Display(Name = "Company")]
  public string? CompanyName { get; set; }
  [Required]
  [Display(Name = "Password")]
  public string? Password { get; set; }
  [Required]
  [Display(Name = "Email")]
  [EmailAddress]
  public string? Email { get; set; }
}

public class PasswordValidation
{
  public int? PasswordScore { get; set; }
  [Required]
  public string? Password { get; set; }
}
