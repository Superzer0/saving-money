using Microsoft.EntityFrameworkCore;
using SavingMoney.WebApi.Model;

namespace SavingMoney.WebApi.Db;

public class SavingMoneyContext : DbContext
{
    public  DbSet<Cost> Costs { get; set; }
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

        modelBuilder.Entity<OrgUser>().Ignore("Temporary. Use identity");

        modelBuilder.Entity<Organization>().HasKey(p => p.Id);
        modelBuilder.Entity<Organization>()
            .HasMany<CostCategory>().WithOne()
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<Organization>()
            .HasMany<Cost>().WithOne()
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<Organization>()
            .HasMany<PredictedSubcategoryCost>().WithOne()
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<CostCategory>().HasKey(p => p.Id);
        modelBuilder.Entity<CostCategory>().HasIndex(p => new
        {
            p.Id,
            OrgId = p.OrganizationId
        }).IsUnique();

        modelBuilder.Entity<CostCategory>()
            .HasMany<CostSubCategory>().WithOne()
            .HasForeignKey(p => p.ParentId).IsRequired();

        modelBuilder.Entity<CostCategory>()
            .HasOne<Organization>()
            .WithMany(p => p.Categories)
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<CostSubCategory>().HasKey(p => p.Id);
        modelBuilder.Entity<CostSubCategory>().HasIndex(p => new
            {p.Id, p.ParentId}).IsUnique();

        modelBuilder.Entity<Cost>().HasKey(p => p.Id);
        modelBuilder.Entity<Cost>().HasIndex(p => new
        {
            p.Id,
            OrgId = p.OrganizationId
        }).IsUnique();

        modelBuilder.Entity<Cost>().HasOne<Organization>()
            .WithMany(p => p.Costs)
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<Cost>().HasOne<CostSubCategory>()
            .WithMany()
            .HasForeignKey(p => p.CostSubCategoryId).IsRequired();


        modelBuilder.Entity<PredictedSubcategoryCost>().HasKey(p => p.Id);
        modelBuilder.Entity<PredictedSubcategoryCost>().HasIndex(p => new
        {
            p.Id,
            OrgId = p.OrganizationId
        }).IsUnique();
        modelBuilder.Entity<PredictedSubcategoryCost>().HasOne<CostSubCategory>()
            .WithMany()
            .HasForeignKey(p => p.CostSubCategoryId).IsRequired();

        modelBuilder.Entity<PredictedSubcategoryCost>().HasOne<Organization>()
            .WithMany(p=> p.PredictedSubcategoryCosts)
            .HasForeignKey(p => p.OrganizationId).IsRequired();

        modelBuilder.Entity<OrgUser>()
            .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
    }
}