using System.ComponentModel.DataAnnotations;

namespace SavingMoney.WebApi.Model;

/// <summary>
/// Users and their costs are divided into organizations (usually family)
/// </summary>
public class Organization
{
    /// <summary>
    /// Organization id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Organization Name
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    /// <summary>
    /// List of organization categories 
    /// </summary>
    public virtual List<CostCategory> Categories { get; set; }
    
    /// <summary>
    /// List of organization costs 
    /// </summary>
    public virtual List<Cost> Costs { get; set; }
    
    /// <summary>
    /// List of organizations predicted costs
    /// </summary>
    public virtual List<PredictedSubcategoryCost> PredictedSubcategoryCosts { get; set; }

    /// <summary>
    /// List of organization users 
    /// </summary>
    public virtual List<OrgUser> OrganizationUsers { get; set; }
}