using System.Text.Json;
using Microsoft.Extensions.Options;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.Categories;

public interface IDefaultCostCategoriesProvider
{
    /// <summary>
    /// Returns default cost categories with subcategories 
    /// </summary>
    /// <param name="currencyType">Requested categories currency that is correlated to language</param>
    /// <returns>List of CostCategories </returns>
    IReadOnlyList<CostCategory> GetDefaultCostCategories(CurrencyType currencyType);
}

/// <summary>
/// Returns default categories and subcategories
/// </summary>
public class DefaultCostCategoriesProvider : IDefaultCostCategoriesProvider
{
    private readonly ILogger<DefaultCostCategoriesProvider> _logger;
    private readonly IHostEnvironment _environment;
    private readonly string DefaultCategoriesFileTemplate;

    public DefaultCostCategoriesProvider(ILogger<DefaultCostCategoriesProvider> logger, IHostEnvironment environment, IOptions<DefaultCategoriesSettings> settings)
    {
        _logger = logger;
        _environment = environment;
        DefaultCategoriesFileTemplate = settings.Value.FileTemplate;
    }
    
    /// <summary>
    /// Returns default cost categories with subcategories 
    /// </summary>
    /// <param name="currencyType">Requested categories currency that is correlated to language</param>
    /// <returns>List of CostCategories </returns>
    public IReadOnlyList<CostCategory> GetDefaultCostCategories(CurrencyType currencyType)
    {
        try
        {
            var defaultCategoriesFullPath = GetFullFilePath();
            _logger.LogInformation("Pulling default categories from {path}", defaultCategoriesFullPath);
            var fileContent = File.ReadAllText(defaultCategoriesFullPath);
            return JsonSerializer.Deserialize<List<CostCategory>>(fileContent) ?? new List<CostCategory>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not fetch default categories");
            throw;
        }

        string GetFullFilePath() => Path.Join(_environment.ContentRootPath, "Categories", "DefaultCategories",
            string.Format(DefaultCategoriesFileTemplate, currencyType.ToString()));
    }
}