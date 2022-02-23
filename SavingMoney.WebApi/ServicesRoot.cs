using Microsoft.AspNetCore.Identity;
using SavingMoney.WebApi.Categories;
using SavingMoney.WebApi.Db;
using SavingMoney.WebApi.Model;
using SavingMoney.WebApi.OrganizationManagement;

namespace SavingMoney.WebApi;

public static class ServicesRoot
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<SavingMoneyContext>();
        serviceCollection.AddIdentity<OrgUser, IdentityRole>()
            .AddEntityFrameworkStores<SavingMoneyContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddAuthentication();

        serviceCollection.AddTransient<IDefaultCostCategoriesProvider, DefaultCostCategoriesProvider>();
        serviceCollection.AddTransient<IOrganizationService, OrganizationService>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddSettings(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        serviceCollection.AddOptions<DefaultCategoriesSettings>().Bind(configurationManager.GetSection("DefaultCategories"));
        return serviceCollection;
    }
}