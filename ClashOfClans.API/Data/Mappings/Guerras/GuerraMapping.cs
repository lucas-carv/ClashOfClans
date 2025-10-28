using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class GuerraMapping : IEntityTypeConfiguration<Guerra>
{
    public void Configure(EntityTypeBuilder<Guerra> builder)
    {
        builder.ToTable("guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder
            .HasIndex(p => new { p.InicioGuerra, p.FimGuerra })
            .IsUnique();
    }
}
public class GuerraClanMapping : IEntityTypeConfiguration<GuerraClan>
{
    public void Configure(EntityTypeBuilder<GuerraClan> builder)
    {
        builder.ToTable("guerra_clan");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder.HasOne(gc => gc.Guerra)
       .WithOne(g => g.GuerraClan)
       .HasForeignKey<GuerraClan>(gc => gc.GuerraId);
    }
}

public class MembroGuerraMapping : IEntityTypeConfiguration<MembroEmGuerra>
{
    public void Configure(EntityTypeBuilder<MembroEmGuerra> builder)
    {
        builder.ToTable("guerra_clan_membro");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

    }
}


public class AtaqueMapping : IEntityTypeConfiguration<Ataque>
{
    public void Configure(EntityTypeBuilder<Ataque> builder)
    {
        builder.ToTable("guerra_membro_ataque");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);
    }
}
