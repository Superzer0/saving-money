using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Model;

public class CostSubCategory
{
    /// <summary>
    /// Subcategory Id 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Parent category id
    /// </summary>
    [Required]
    public int ParentId { get; set; }

    /// <summary>
    /// Subcategory name
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Subcategory description
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}