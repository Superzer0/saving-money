using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SavingMoney.WebApi.Categories;
using SavingMoney.WebApi.Controllers;
using SavingMoney.WebApi.Db;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.OrganizationManagement;

public interface IOrganizationService
{
    /// <summary>
    /// Creates organization
    /// </summary>
    /// <param name="orgCreateModel">Organization information</param>
    /// <returns>Created Org</returns>
    Task<Organization> CreateOrganization(OrgCreateModel orgCreateModel);
}

public partial class OrganizationService : IOrganizationService
{
    private readonly ILogger<OrganizationService> _logger;
    private readonly IDefaultCostCategoriesProvider _defaultCostCategoriesProvider;
    private readonly SavingMoneyContext _savingMoneyContext;
    private readonly UserManager<OrgUser> _userManager;

    public OrganizationService(ILogger<OrganizationService> logger,
        IDefaultCostCategoriesProvider defaultCostCategoriesProvider, SavingMoneyContext savingMoneyContext,
        UserManager<OrgUser> userManager)
    {
        _logger = logger;
        _defaultCostCategoriesProvider = defaultCostCategoriesProvider;
        _savingMoneyContext = savingMoneyContext;
        _userManager = userManager;
    }

    [LoggerMessage(0, LogLevel.Information, "Created new Org {org}")]
    partial void LogOrgCreated(ILogger logger, Organization org);

    /// <summary>
    /// Creates organization
    /// </summary>
    /// <param name="org">Organization information</param>
    /// <param name="firstUser">First user information</param>
    /// <returns>Created Org</returns>
    public async Task<Organization> CreateOrganization(OrgCreateModel orgCreateModel)
    {
        var org = new Organization
        {
            Name = orgCreateModel.Name
        };
        
        var firstUser = new OrgUser
        {
            Email = orgCreateModel.FirstUser.Email ,
            UserName = orgCreateModel.FirstUser.Email,
            FirstName = orgCreateModel.FirstUser.FirstName,
            LastName = orgCreateModel.FirstUser.LastName
        };

        var validationModel = await ValidateNewOrganization(org.Name, firstUser, orgCreateModel.FirstUser.Password);

        if (validationModel.HasErrors)
        {
            throw new OrgValidationException(validationModel);
        }

        var defaultCategories = _defaultCostCategoriesProvider.GetDefaultCostCategories(org.DefaultCurrency);
        org.Categories = defaultCategories.ToList();

        await using var transaction = await _savingMoneyContext.Database.BeginTransactionAsync();
        try
        {
            await _savingMoneyContext.Organizations.AddAsync(org);
            await _savingMoneyContext.SaveChangesAsync();
            firstUser.OrganizationId = org.Id;
            org.OrganizationUsers = new List<OrgUser> {firstUser};
            var results = await _userManager.CreateAsync(firstUser, orgCreateModel.FirstUser.Password);
            if (!results.Succeeded)
            {
                throw new OrgValidationException(new OrgValidationModel
                {
                    NewUserValidation = results.Errors.ToList()
                });
            }

            await transaction.CommitAsync();
            LogOrgCreated(_logger, org);
            return org;
        }
        catch (OrgValidationException e)
        {
            _logger.LogInformation(e,
                "Could not create new organization because of user validation errors {orgName}, {firstUser}", org.Name,
                firstUser.Email);
            await transaction.RollbackAsync();
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not create new organization. Rolling back the transaction");
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Validates new organization
    /// </summary>
    /// <param name="org">Organization information</param>
    /// <param name="firstUser">First user information/param>
    /// <returns>OrgValidationModel</returns>
    public async Task<OrgValidationModel> ValidateNewOrganization(string orgName, OrgUser orgUser, string password)
    {
        var userValidationResults = new List<IdentityError>();
        foreach (var validator in _userManager.PasswordValidators)
        {
            var results = await validator.ValidateAsync(_userManager, orgUser, password);
            userValidationResults.AddRange(results.Errors);
        }

        return new OrgValidationModel
        {
            OrgNameTaken = await _savingMoneyContext.Organizations.AnyAsync(p => p.Name == orgName),
            UserEmailTaken = (await _userManager.FindByEmailAsync(orgUser.Email)) != null,
            NewUserValidation = userValidationResults
        };
    }
}