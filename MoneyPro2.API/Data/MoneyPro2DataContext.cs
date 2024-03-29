﻿using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data.Mappings;
using MoneyPro2.Domain.Entities;
using MoneyPro2.Domain.ValueObjects;

namespace MoneyPro2.API.Data;

public class MoneyPro2DataContext : DbContext
{
    public MoneyPro2DataContext() { }

    public MoneyPro2DataContext(DbContextOptions<MoneyPro2DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserLogin> UserLogins { get; set; } = null!;
    public DbSet<InstitutionType> InstitutionTypes { get; set; } = null!;
    public DbSet<Institution> Institutions { get; set; } = null!;
    public DbSet<Coin> Coins { get; set; } = null!;
    public DbSet<AccountGroup> AccountGroups { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ignorar ValueObjects
        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Email>();
        modelBuilder.Ignore<CPF>();

        // Sequencias
        modelBuilder.HasSequence<int>("Seq_MoedaID").StartsAt(1).IncrementsBy(1);
        modelBuilder.HasSequence<int>("Seq_GrupoContaID").StartsAt(1).IncrementsBy(1);

        // UserLogin - User
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.UserLogins)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .HasConstraintName("FK_UserLogin_User_UserId")
            .OnDelete(DeleteBehavior.Cascade);

        // TipoInstituicao - User
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.InstitutionTypes)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .HasConstraintName("FK_TipoInstituicao_User_UserId")
            .OnDelete(DeleteBehavior.Cascade);

        // Insituicao - User
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.Institutions)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .HasConstraintName("FK_Instituicao_User_UserId")
            .OnDelete(DeleteBehavior.Cascade);

        // Instituicao - TipoInstituicao
        modelBuilder
            .Entity<InstitutionType>()
            .HasMany(e => e.Institutions)
            .WithOne(e => e.InstitutionType)
            .HasForeignKey(e => e.TipoInstituicaoId)
            .IsRequired(false)
            .HasConstraintName("FK_Instituicao_TipoInstituicao_TipoInstituicaoId")
            .OnDelete(DeleteBehavior.NoAction);

        // GrupoDeConta - User
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.AccountGroups)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UsuarioId)
            .IsRequired(false)
            .HasConstraintName("FK_AccountGroup_User_UsuarioID")
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new UserLoginMap());
        modelBuilder.ApplyConfiguration(new InstitutionTypeMap());
        modelBuilder.ApplyConfiguration(new InstitutionMap());
        modelBuilder.ApplyConfiguration(new CoinMap());
        modelBuilder.ApplyConfiguration(new AccountGroupMap());
    }
}
