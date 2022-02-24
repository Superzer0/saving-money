using Microsoft.AspNetCore.Identity;

namespace SavingMoney.WebApi.OrganizationManagement;

public class OrgValidationModel
{
    public bool OrgNameTaken { get; set; }
    public bool UserEmailTaken { get; set; }
    public List<IdentityError> NewUserValidation { get; set; } = new List<IdentityError>();
    public bool HasErrors => OrgNameTaken || UserEmailTaken || NewUserValidation.Any();
}