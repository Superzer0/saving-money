using Microsoft.Build.Framework;

namespace SavingMoney.WebApi.Controllers;

public class OrgCreateModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public UserRegisterModel FirstUser { get; set; }
    
}