using ClashOfClans.API.Model.LigaDeClans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.LigaDeGuerras;

public class LigaDeGuerrasMapping : IEntityTypeConfiguration<LigaDeGuerra>
{
    public void Configure(EntityTypeBuilder<LigaDeGuerra> builder)
    {
        builder.ToTable("liga_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder
            .HasIndex(p => new { p.Temporada })
            .IsUnique();
    }
}
