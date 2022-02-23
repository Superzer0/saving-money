

using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Controllers;

/// <summary>
/// Model used to create an organization 
/// </summary>
public class OrgCreateModel
{
    /// <summary>
    /// Organization name. Must be unique 
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// First organization user
    /// </summary>
    [Required]
    public UserRegisterModel FirstUser { get; set; }
    
}