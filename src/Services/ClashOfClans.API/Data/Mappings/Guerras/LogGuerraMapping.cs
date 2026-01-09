using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class LogGuerraMapping : IEntityTypeConfiguration<LogGuerra>
{
    public void Configure(EntityTypeBuilder<LogGuerra> builder)
    {
        builder.HasOne(g => g.Oponente)
               .WithOne()
               .HasForeignKey<LogOponenteGuerra>(c => c.LogGuerraId);

        builder.HasOne(g => g.Clan)
               .WithOne()
               .HasForeignKey<LogClanGuerra>(c => c.LogGuerraId);

        builder.ToTable("log_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

    }
}

public class LogClanGuerraMapping : IEntityTypeConfiguration<LogClanGuerra>
{
    public void Configure(EntityTypeBuilder<LogClanGuerra> builder)
    {
        builder.ToTable("log_clan_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);
    }
}

public class LogOponenteGuerraMapping : IEntityTypeConfiguration<LogOponenteGuerra>
{
    public void Configure(EntityTypeBuilder<LogOponenteGuerra> builder)
    {
        builder.ToTable("log_oponente_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);
    }
}
