using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Model;

/// <summary>
/// Category of the cost spent
/// </summary>
public class CostCategory
{
    /// <summary>
    /// Category Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Organization Id
    /// </summary>
    [Required]
    public int OrganizationId { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Category description
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }
    
    /// <summary>
    /// Cost can be also an income.  
    /// </summary>
    [Required]
    public bool IsIncome { get; set; }

    public List<CostSubCategory> SubCategories { get; set; }
}