using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class ClanEmGuerraMapping : IEntityTypeConfiguration<ClanEmGuerra>
{
    public void Configure(EntityTypeBuilder<ClanEmGuerra> builder)
    {
        builder.HasOne(c => c.Guerra)
               .WithMany(g => g.ClansEmGuerra)
               .HasForeignKey(c => c.GuerraId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("clan_em_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Guerra)
            .WithMany() 
            .HasForeignKey(c => c.GuerraId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => new { p.GuerraId, p.Tag })
            .IsUnique();

        builder.Property(m => m.Tipo)
               .HasConversion<string>()
               .HasMaxLength(20)
               .HasColumnType("varchar(20)");
    }
}
