using ClashOfClans.Integracao.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.Integracao.API.Data.Mappings;

public class ClanMapping : IEntityTypeConfiguration<Clan>
{
    public void Configure(EntityTypeBuilder<Clan> builder)
    {
        builder.ToTable("clan");
        builder.HasKey(c => c.Tag);

        builder.HasOne(c => c.BadgeUrls)
               .WithOne(b => b.Clan)
               .HasForeignKey<BadgeUrls>(b => b.ClanTag);
    }
}
