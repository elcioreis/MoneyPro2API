﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.Domain.Entities;

namespace MoneyPro2.API.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Tabela
        builder.ToTable("User");

        // Chave primária
        builder.HasKey(x => x.UserId);

        // Identidade
        builder.Property(x => x.UserId).ValueGeneratedOnAdd().UseIdentityColumn();

        // Campos
        builder
            .Property(x => x.Username)
            .IsRequired(true)
            .HasColumnName("UserName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder
            .Property(x => x.Nome)
            .IsRequired(true)
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder
            .OwnsOne(o => o.Email)
            .Property(p => p.Address)
            .IsRequired(true)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(200);

        builder
            .OwnsOne(o => o.CPF)
            .Property(p => p.Conteudo)
            .IsRequired(true)
            .HasColumnName("CPF")
            .HasColumnType("CHAR")
            .HasMaxLength(11);

        builder
            .Property(x => x.Criptografada)
            .IsRequired(true)
            .HasColumnName("PasswordHash")
            .HasColumnType("CHAR")
            .HasMaxLength(32);

        builder.Ignore(x => x.Senha);

        // Criando indices
        builder.HasIndex(x => x.Username, "IX_User_Username").IsUnique();

        // Criando indices para ValueObject
        builder
            .OwnsOne(p => p.Email)
            .HasIndex(i => i.Address)
            .IsClustered(false)
            .HasDatabaseName("IX_User_Email")
            .IsUnique();

        builder
            .OwnsOne(p => p.CPF)
            .HasIndex(i => i.Conteudo)
            .IsClustered(false)
            .HasDatabaseName("IX_User_CPF")
            .IsUnique();
    }
}
