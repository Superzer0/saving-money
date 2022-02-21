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
    public int OrgId { get; set; }
    
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
}