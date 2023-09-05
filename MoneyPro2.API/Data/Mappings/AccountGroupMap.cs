using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.Domain.Entities;

namespace MoneyPro2.API.Data.Mappings;

public class AccountGroupMap : IEntityTypeConfiguration<AccountGroup>
{
    public void Configure(EntityTypeBuilder<AccountGroup> builder)
    {
        // Tabela
        builder.ToTable("GrupoConta");

        // Chave primária
        builder.HasKey(x => x.GrupoContaId);

        // Identidade
        builder
            .Property(x => x.GrupoContaId)
            .ValueGeneratedOnAdd()
            .UseSequence("Seq_GrupoContaID");

        // Campos
        builder
            .Property(x => x.UsuarioId)
            .IsRequired(true)
            .HasColumnName("UsuarioId")
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
            .Property(x => x.Ordem)
            .IsRequired(true)
            .HasColumnName("Ordem")
            .HasColumnType("INT")
            .HasDefaultValueSql("0");

        builder
            .Property(x => x.Ativo)
            .IsRequired(true)
            .HasColumnName("Ativo")
            .HasColumnType("BIT")
            .HasDefaultValueSql("1");

        builder
            .Property(x => x.Painel)
            .IsRequired(true)
            .HasColumnName("Painel")
            .HasColumnType("BIT")
            .HasDefaultValueSql("0");

        builder
            .Property(x => x.FluxoDisponivel)
            .IsRequired(true)
            .HasColumnName("FluxoDisponivel")
            .HasColumnType("BIT")
            .HasDefaultValueSql("0");

        builder
            .Property(x => x.FluxoCredito)
            .IsRequired(true)
            .HasColumnName("FluxoCredito")
            .HasColumnType("BIT")
            .HasDefaultValueSql("0");

        // Criando indices
        builder
            .HasIndex(x => new { x.UsuarioId, x.Apelido }, "IX_AccountGroup_UsuarioID_Apelido")
            .IsUnique(true)
            .IsClustered(false);

        builder
            .HasIndex(x => new { x.UsuarioId, x.Ordem }, "IX_AccountGroup_UsuarioID_Ordem")
            .IsUnique(false)
            .IsClustered(false);
    }
}
