using Microsoft.EntityFrameworkCore;
using Trc.Core.Models;

namespace Trc.Core.Data;

/// <summary>Контекст бази даних ТРЦ (Entity Framework Core, SQLite).</summary>
public class TrcDbContext : DbContext
{
    public TrcDbContext(DbContextOptions<TrcDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Premises> Premises => Set<Premises>();
    public DbSet<Lease> Leases => Set<Lease>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<MallEvent> Events => Set<MallEvent>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<ParkingSession> ParkingSessions => Set<ParkingSession>();

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        // SQLite не має типу decimal, тому для коректних SUM/ORDER BY зберігаємо гроші як REAL.
        builder.Properties<decimal>().HaveConversion<double>();

        // Переліки зберігаємо рядками — так базу зручніше переглядати.
        builder.Properties<UserRole>().HaveConversion<string>();
        builder.Properties<PremisesType>().HaveConversion<string>();
        builder.Properties<PremisesStatus>().HaveConversion<string>();
        builder.Properties<TenantCategory>().HaveConversion<string>();
        builder.Properties<LeaseStatus>().HaveConversion<string>();
        builder.Properties<PaymentStatus>().HaveConversion<string>();
        builder.Properties<Department>().HaveConversion<string>();
        builder.Properties<EventStatus>().HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>(e =>
        {
            e.Property(u => u.Login).HasMaxLength(30).IsRequired();
            e.Property(u => u.FullName).HasMaxLength(100).IsRequired();
            e.HasIndex(u => u.Login).IsUnique();
        });

        mb.Entity<Tenant>(e =>
        {
            e.Property(t => t.CompanyName).HasMaxLength(150).IsRequired();
            e.Property(t => t.TaxCode).HasMaxLength(10).IsRequired();
            e.Property(t => t.ContactPerson).HasMaxLength(100);
            e.Property(t => t.Phone).HasMaxLength(20);
            e.Property(t => t.Email).HasMaxLength(100);
            e.HasIndex(t => t.TaxCode).IsUnique();
        });

        mb.Entity<Premises>(e =>
        {
            e.Property(p => p.Number).HasMaxLength(10).IsRequired();
            e.Property(p => p.Description).HasMaxLength(300);
            e.HasIndex(p => p.Number).IsUnique();
            e.Ignore(p => p.BaseMonthlyRent);
        });

        mb.Entity<Lease>(e =>
        {
            e.Property(l => l.Number).HasMaxLength(20).IsRequired();
            e.HasIndex(l => l.Number).IsUnique();
            e.HasOne(l => l.Tenant).WithMany(t => t.Leases)
                .HasForeignKey(l => l.TenantId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(l => l.Premises).WithMany(p => p.Leases)
                .HasForeignKey(l => l.PremisesId).OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<Payment>(e =>
        {
            e.HasOne(p => p.Lease).WithMany(l => l.Payments)
                .HasForeignKey(p => p.LeaseId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(p => new { p.LeaseId, p.PeriodYear, p.PeriodMonth }).IsUnique();
        });

        mb.Entity<Employee>(e =>
        {
            e.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Position).HasMaxLength(100).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(20);
        });

        mb.Entity<MallEvent>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(150).IsRequired();
            e.Property(x => x.Location).HasMaxLength(100).IsRequired();
            e.Property(x => x.Description).HasMaxLength(500);
        });

        mb.Entity<Ticket>(e =>
        {
            e.Property(t => t.BuyerName).HasMaxLength(100);
            e.HasOne(t => t.Event).WithMany(ev => ev.Tickets)
                .HasForeignKey(t => t.MallEventId).OnDelete(DeleteBehavior.Cascade);
        });

        mb.Entity<ParkingSession>(e =>
        {
            e.Property(s => s.PlateNumber).HasMaxLength(10).IsRequired();
            e.HasIndex(s => s.PlateNumber);
            e.Ignore(s => s.IsActive);
        });
    }
}
