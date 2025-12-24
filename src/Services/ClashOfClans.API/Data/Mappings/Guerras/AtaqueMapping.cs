using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class AtaqueMapping : IEntityTypeConfiguration<GuerraMembroAtaque>
{
    public void Configure(EntityTypeBuilder<GuerraMembroAtaque> builder)
    {
        builder.ToTable("guerra_membro_ataque");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder.HasIndex(a => new { a.AtacanteTag, a.DefensorTag, a.MembroEmGuerraId })
              .IsUnique();
    }
}
