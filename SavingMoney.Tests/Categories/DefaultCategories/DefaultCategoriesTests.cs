using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SavingMoney.WebApi.Categories;
using SavingMoney.WebApi.Model;
using Xunit;

namespace SavingMoney.Tests.Categories;

public class DefaultCategoriesTests
{
    private readonly Mock<IHostEnvironment> _hostEnvironmentMock;
    private readonly Mock<IOptions<DefaultCategoriesSettings>> _defaultCategoriesMock;
    public DefaultCategoriesTests()
    {
        _hostEnvironmentMock = new Mock<IHostEnvironment>();
        _defaultCategoriesMock = new Mock<IOptions<DefaultCategoriesSettings>>();
    }

    private IDefaultCostCategoriesProvider CreateSut()
    {
        return new DefaultCostCategoriesProvider(new Mock<ILogger<DefaultCostCategoriesProvider>>().Object,
            _hostEnvironmentMock.Object, _defaultCategoriesMock.Object);
    }

    [Fact]
    public void CorrectFileInPlace_DeserializesCorrectly()
    {
        var testLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var testedCurrency = CurrencyType.PLN;
        _hostEnvironmentMock.SetupGet(environment => environment.ContentRootPath).Returns(testLocation);
        _defaultCategoriesMock.SetupGet(p => p.Value).Returns(new DefaultCategoriesSettings
        {
            FileTemplate = "TestingDefaultCategories_{0}.json"
        });
        
        var sut = CreateSut();
        var results = sut.GetDefaultCostCategories(testedCurrency);
        var expectedResults = new List<CostCategory>
        {
            new()
            {
                Name = "Income",
                Description = "Our income",
                IsIncome = true,
                SubCategories = new List<CostSubCategory>
                {
                    new()
                    {
                        Name = "Income",
                        Description = "Main income"
                    }
                }
            },
            new()
            {
                Name = "Food",
                Description = "Food costs",
                IsIncome = false,
                SubCategories = new List<CostSubCategory>
                {
                    new()
                    {
                        Name = "Home",
                        Description = "Food at home"
                    },
                    new()
                    {
                        Name = "City",
                        Description = "Food in the city"
                    }
                }
            }
        };

        results.Should().BeEquivalentTo(expectedResults);
    }
}