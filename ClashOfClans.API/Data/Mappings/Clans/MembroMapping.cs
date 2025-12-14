using ClashOfClans.API.Model.Clans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Clans;

public class MembroMapping : IEntityTypeConfiguration<Membro>
{
    public void Configure(EntityTypeBuilder<Membro> builder)
    {
        builder.ToTable("membro");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder
            .HasIndex(p => p.Tag)
            .IsUnique();

        builder.Property(m => m.Situacao)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasColumnType("varchar(20)");
    }
}
