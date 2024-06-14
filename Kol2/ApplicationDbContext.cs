using Microsoft.EntityFrameworkCore;
using Kol2.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Subscriptions)
            .WithOne();

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Payments)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.IdClient);

        modelBuilder.Entity<Subscription>()
            .HasMany(s => s.Payments)
            .WithOne(p => p.Subscription)
            .HasForeignKey(p => p.IdSubscription);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Client)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.IdClient);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Subscription)
            .WithMany(s => s.Sales)
            .HasForeignKey(s => s.IdSale);
    }
}