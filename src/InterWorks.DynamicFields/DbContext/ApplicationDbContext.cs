using InterWorks.DynamicFields.Models;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.DbContext;

public sealed class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Constants.Database.Name);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerField> CustomerFields { get; set; }
    public DbSet<FieldValueHistory> FieldValueHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasMany(x => x.CustomerFields)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CustomerField>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.CustomerFields)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FieldValueHistory>()
            .HasOne(x => x.CustomerField)
            .WithMany(x => x.FieldValueHistories)
            .HasForeignKey(x => x.CustomerFieldId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}