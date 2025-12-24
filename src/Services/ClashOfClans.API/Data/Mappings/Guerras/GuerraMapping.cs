using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class GuerraMapping : IEntityTypeConfiguration<Guerra>
{
    public void Configure(EntityTypeBuilder<Guerra> builder)
    {
        builder.HasOne(g => g.ClanEmGuerra)
               .WithOne()
               .HasForeignKey<ClanEmGuerra>(c => c.GuerraId);

        builder.ToTable("guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);
    }
}
