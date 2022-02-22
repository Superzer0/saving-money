using Microsoft.AspNetCore.Identity;
using SavingMoney.WebApi.Db;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi;

public static class ServicesRoot
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<SavingMoneyContext>();
        serviceCollection.AddDbContext<SavingMoneyContext>();
        serviceCollection.AddIdentity<OrgUser, IdentityRole>()
            .AddEntityFrameworkStores<SavingMoneyContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddAuthentication();
        
        return serviceCollection;
    }
    
}