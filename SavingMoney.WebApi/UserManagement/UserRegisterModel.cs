using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Controllers;

public class UserRegisterModel
{
    [Required]
    public int OrganizationId { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
}