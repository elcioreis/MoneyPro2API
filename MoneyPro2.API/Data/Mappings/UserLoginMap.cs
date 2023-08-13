using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.API.Models;

namespace MoneyPro2.API.Data.Mappings;

public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        // Tabela
        builder.ToTable("UserLogin");

        // Chave primária
        builder.HasKey(x => x.Id);

        // Identidade
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        // Campos
        builder
            .Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnName("UserId")
            .HasColumnType("INT");

        builder
            .Property(x => x.LoginTime)
            .IsRequired(true)
            .HasColumnName("LoginTime")
            .HasColumnType("DATETIME")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        // Criando indices
        builder
            .HasIndex(x => new { x.UserId, x.LoginTime }, "IX_UserLogin_UserId")
            .IsClustered(false);
    }
}
