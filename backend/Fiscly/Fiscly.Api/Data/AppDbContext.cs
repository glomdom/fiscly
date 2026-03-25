using Fiscly.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fiscly.Api.Data;

public sealed class AppDbContext : DbContext {
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity => {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Transaction>(entity => {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Merchant)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(x => x.Category)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            entity.Property(x => x.Date)
                .IsRequired();

            entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}