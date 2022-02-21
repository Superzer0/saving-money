using Microsoft.EntityFrameworkCore;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.Db;

public class SavingMoneyContext : DbContext
{
    public DbSet<Cost> Costs { get; set; }
    public DbSet<CostCategory> CostCategories { get; set; }
    public DbSet<CostSubCategory> CostSubCategories { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<PredictedSubcategoryCost> PredictedSubcategoryCosts { get; set; }

    public string DbPath { get; init; }

    public SavingMoneyContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "SavingMoneyContext.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SavingMoney");

        modelBuilder.Entity<Organization>().HasKey(p => p.Id);
        modelBuilder.Entity<Organization>()
            .HasMany<CostCategory>().WithOne()
            .HasForeignKey(p => p.OrgId)
            .HasPrincipalKey(p => p.Id);

        modelBuilder.Entity<Organization>()
            .HasMany<Cost>().WithOne()
            .HasForeignKey(p => p.OrgId)
            .HasPrincipalKey(p => p.Id);

        modelBuilder.Entity<Organization>()
            .HasMany<PredictedSubcategoryCost>().WithOne()
            .HasForeignKey(p => p.OrgId)
            .HasPrincipalKey(p => p.Id);
        
        modelBuilder.Entity<CostCategory>().HasKey(p => p.Id);
        modelBuilder.Entity<CostCategory>().HasIndex(p => new
            {p.Id, p.OrgId}).IsUnique();

        modelBuilder.Entity<CostCategory>()
            .HasMany<CostSubCategory>().WithOne()
            .HasForeignKey(p => p.ParentId)
            .HasPrincipalKey(p => p.Id);

        modelBuilder.Entity<CostSubCategory>().HasKey(p => p.Id);
        modelBuilder.Entity<CostSubCategory>().HasIndex(p => new
            {p.Id, p.ParentId}).IsUnique();

        modelBuilder.Entity<Cost>().HasKey(p => p.Id);
        modelBuilder.Entity<Cost>().HasIndex(p => new
            {p.Id, p.OrgId}).IsUnique();
        modelBuilder.Entity<Cost>().HasOne<CostSubCategory>()
            .WithMany()
            .HasForeignKey(p => p.CostSubCategoryId)
            .HasPrincipalKey(p => p.Id);
        

        modelBuilder.Entity<PredictedSubcategoryCost>().HasKey(p => p.Id);
        modelBuilder.Entity<PredictedSubcategoryCost>().HasIndex(p => new
            {p.Id, p.OrgId}).IsUnique();
        modelBuilder.Entity<PredictedSubcategoryCost>().HasOne<CostSubCategory>()
            .WithMany()
            .HasForeignKey(p => p.CostSubCategoryId)
            .HasPrincipalKey(p => p.Id);

        modelBuilder.Entity<OrgUser>()
            .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
    }
}