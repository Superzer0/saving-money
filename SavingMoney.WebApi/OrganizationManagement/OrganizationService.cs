using SavingMoney.WebApi.Categories;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.OrganizationManagement;

public interface IOrganizationService
{
    /// <summary>
    /// Creates organization
    /// </summary>
    /// <param name="org">Organization information</param>
    /// <param name="firstUser">First user information</param>
    /// <returns>Created Org</returns>
    Organization CreateOrganization(Organization org, OrgUser firstUser);
}

public class OrganizationService : IOrganizationService
{
    private readonly ILogger<OrganizationService> _logger;
    private readonly IDefaultCostCategoriesProvider _defaultCostCategoriesProvider;

    public OrganizationService(ILogger<OrganizationService> logger, IDefaultCostCategoriesProvider defaultCostCategoriesProvider)
    {
        _logger = logger;
        _defaultCostCategoriesProvider = defaultCostCategoriesProvider;
    }

    /// <summary>
    /// Creates organization
    /// </summary>
    /// <param name="org">Organization information</param>
    /// <param name="firstUser">First user information</param>
    /// <returns>Created Org</returns>
    public Organization CreateOrganization(Organization org, OrgUser firstUser)
    {
        
        return new Organization();
    }
    
    
}