namespace SavingMoney.WebApi.OrganizationManagement;

/// <summary>
/// Response containing information about created organization
/// </summary>
public class OrgCreatedResponse
{
    /// <summary>
    /// Newly created organization
    /// </summary>
    public int OrgId { get; set; }
    
    /// <summary>
    /// Newly created organization user
    /// </summary>
    public string FirstUserId { get; set; }
}