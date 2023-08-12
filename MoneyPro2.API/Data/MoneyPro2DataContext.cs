using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data.Mappings;
using MoneyPro2.API.Models;
using MoneyPro2.API.ValueObjects;

namespace MoneyPro2.API.Data;

public class MoneyPro2DataContext : DbContext
{
    public MoneyPro2DataContext() { }

    public MoneyPro2DataContext(DbContextOptions<MoneyPro2DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserLogin> UserLogins { get; set; } = null!;

    //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(
    //        "Server=localhost;Database=MoneyPro2_Devel;Integrated Security=True;Trust Server Certificate=true"
    //    );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ignorar ValueObjects
        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Email>();
        modelBuilder.Ignore<CPF>();

        // Chaves estrangeiras
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.UserLogins)
            .WithOne(e => e.Users)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new UserLoginMap());
    }
}
