using ClashOfClans.Integracao.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.Integracao.API.Data.Mappings;

public class GuerraMapping : IEntityTypeConfiguration<Guerra>
{
    public void Configure(EntityTypeBuilder<Guerra> builder)
    {
        builder.ToTable("guerra");
        builder.HasKey(c => c.Id);

        builder.HasOne(g => g.Clan)
            .WithMany()
            .HasForeignKey("ClanId");

        builder.HasOne(g => g.Opponent)
               .WithMany()
               .HasForeignKey("OpponentId");
    }
}
