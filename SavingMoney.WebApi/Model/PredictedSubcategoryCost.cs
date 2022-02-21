using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Model;

public class PredictedSubcategoryCost
{
    // <summary>
    /// Entity id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Organization
    /// </summary>
    [Required]
    public int OrgId { get; set; }
    
    /// <summary>
    /// Predicted cost subcategory id
    /// </summary>
    [Required]
    public int CostSubCategoryId { get; set; }
    
    /// <summary>
    /// Who added the predicted cost 
    /// </summary>
    [Required]
    public int AddedBy { get; set; }
    
    /// <summary>
    /// When the cost was spent 
    /// </summary>
    [Required]
    public DateTime PredictedMonth { get; set; }
    
    /// <summary>
    /// Comment explaining the predicted category cost
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Comment { get; set; }
    
    /// <summary>
    /// Amount to spent
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Type of currency
    /// </summary>
    [Required]
    public CurrencyType Currency { get; set; }
}