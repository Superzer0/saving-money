using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Controllers;

public class UserRegisterModel
{
    /// <summary>
    /// User email address
    /// </summary>
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    [Required]
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last Name
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    [Required]
    public string Password { get; set; }
}