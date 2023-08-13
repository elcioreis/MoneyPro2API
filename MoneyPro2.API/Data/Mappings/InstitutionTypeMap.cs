using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.API.Models;

namespace MoneyPro2.API.Data.Mappings;

public class InstitutionTypeMap : IEntityTypeConfiguration<InstitutionType>
{
    public void Configure(EntityTypeBuilder<InstitutionType> builder)
    {
        // Identifica a tabela
        builder.ToTable("TipoInstituicao");
        // Cria a chave primária
        builder.HasKey(x => x.TipoInstituicaoId);
        // Geração da ID pelo sql server
        builder.Property(x => x.TipoInstituicaoId).ValueGeneratedOnAdd().UseIdentityColumn();

        // Campos
        builder
            .Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnName("UserId")
            .HasColumnType("INT");

        builder
            .Property(x => x.Apelido)
            .IsRequired(true)
            .HasColumnName("Apelido")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(40);

        builder
            .Property(x => x.Descricao)
            .IsRequired(true)
            .HasColumnName("Descricao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder
            .Property(x => x.Ativo)
            .IsRequired(true)
            .HasColumnName("Ativo")
            .HasColumnType("BIT")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("1");

        // Criando indices
        builder
            .HasIndex(x => new { x.UserId, x.Apelido }, "IX_TipoInstituicao_UserId_Apelido")
            .IsUnique(true)
            .IsClustered(false);

        //// Chaves estrangeiras
        //builder
        //    .Entity<User>()
        //    .HasMany(e => e.UserLogins)
        //    .WithOne(e => e.Users)
        //    .HasForeignKey(e => e.UserId)
        //    .IsRequired(false)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
