﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.Models;

namespace MoneyPro2.Data.Mappings;
public class InstitutionTypeMap : IEntityTypeConfiguration<InstitutionType>
{
    public void Configure(EntityTypeBuilder<InstitutionType> builder)
    {
        builder.ToTable("InstitutionType");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("INT");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(40);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);

        builder.Property(x => x.Active)
            .IsRequired()
            .HasColumnName("Active")
            .HasColumnType("BIT")
            .HasDefaultValueSql("1");

        builder.HasIndex(x => new { x.UserId, x.Name }, "IX_InstitutionType_UserId_Name").IsUnique();

        builder
            .HasMany(x => x.Institutions)
            .WithOne(x => x.InstitutionType)
            .HasForeignKey("InstitutionTypeId")
            .HasConstraintName("Fk_Institution_InstitutionType")
            .OnDelete(DeleteBehavior.NoAction);
    }
}