using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class ClanEmGuerraMapping : IEntityTypeConfiguration<ClanEmGuerra>
{
    public void Configure(EntityTypeBuilder<ClanEmGuerra> builder)
    {
        builder.ToTable("clan_em_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder.HasOne(gc => gc.Guerra)
           .WithOne(g => g.ClanEmGuerra)
           .HasForeignKey<ClanEmGuerra>(gc => gc.GuerraId);

        builder
            .HasIndex(p => new { p.GuerraId, p.Tag })
            .IsUnique();
    }
}
