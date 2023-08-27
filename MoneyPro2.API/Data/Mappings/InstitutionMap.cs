using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.Domain.Entities;

namespace MoneyPro2.API.Data.Mappings;

public class InstitutionMap : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        // Identifica a tabela
        builder.ToTable("Instituicao");
        // Cria a chave primária
        builder.HasKey(x => x.InstituicaoId);
        // Geração da ID pelo sql server
        builder.Property(x => x.InstituicaoId).ValueGeneratedOnAdd().UseIdentityColumn();

        // Campos
        builder
            .Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnName("UserId")
            .HasColumnType("INT");

        builder
            .Property(x => x.TipoInstituicaoId)
            .IsRequired(true)
            .HasColumnName("TipoInstituicaoId")
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
            .HasIndex(
                x =>
                    new
                    {
                        x.UserId,
                        x.TipoInstituicaoId,
                        x.Apelido
                    },
                "IX_Instituicao_UserId_TipoInstituicaoId_Apelido"
            )
            .IsUnique(true)
            .IsClustered(false);
    }
}
