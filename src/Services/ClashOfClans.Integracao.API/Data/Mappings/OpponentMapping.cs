using ClashOfClans.Integracao.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.Integracao.API.Data.Mappings;

public class OpponentMapping : IEntityTypeConfiguration<Opponent>
{
    public void Configure(EntityTypeBuilder<Opponent> builder)
    {
        builder.ToTable("opponent");
        builder.HasKey(o => o.Tag);

        builder.HasOne(c => c.BadgeUrls)
               .WithOne(b => b.Opponent)
               .HasForeignKey<BadgeUrls>(b => b.ClanTag);
    }
}