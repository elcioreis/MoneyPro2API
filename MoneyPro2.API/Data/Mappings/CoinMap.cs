using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyPro2.API.Models;

namespace MoneyPro2.API.Data.Mappings;

public class CoinMap : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        // Tabela
        builder.ToTable("Moeda");

        // Chave primária
        builder.HasKey(x => x.MoedaId);

        // Identidade
        builder.Property(x => x.MoedaId).ValueGeneratedOnAdd().UseSequence("Seq_MoedaID");

        // Campos
        builder
            .Property(x => x.Apelido)
            .IsRequired(true)
            .HasColumnName("Apelido")
            .HasColumnType("VARCHAR")
            .HasMaxLength(40);

        builder
            .Property(x => x.Simbolo)
            .IsRequired(true)
            .HasColumnName("Simbolo")
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);

        builder
            .Property(x => x.Padrao)
            .IsRequired(true)
            .HasColumnName("Padrao")
            .HasColumnType("BIT");

        builder
            .Property(x => x.MoedaVirtual)
            .IsRequired(true)
            .HasColumnName("MoedaVirtual")
            .HasColumnType("BIT");

        builder
            .Property(x => x.BancoCentral)
            .IsRequired(false)
            .HasColumnName("BancoCentral")
            .HasColumnType("INT");

        builder
            .Property(x => x.Eletronica)
            .IsRequired(false)
            .HasColumnName("Eletronica")
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);

        builder
            .Property(x => x.Observacao)
            .IsRequired(false)
            .HasColumnName("Observacao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        // Indices
        builder.HasIndex(x => x.Apelido, "IX_Coin_Apelido").IsUnique();
        builder.HasIndex(x => x.Simbolo, "IX_Coin_Simbolo").IsUnique();
    }
}
