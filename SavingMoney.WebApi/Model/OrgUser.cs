using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SavingMoney.WebApi.Model;

public class OrgUser : IdentityUser
{
    /// <summary>
    /// Organization where user is linked 
    /// </summary>
    [Required]
    public int OrganizationId { get; set; }
}