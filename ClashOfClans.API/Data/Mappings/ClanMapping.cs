using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings;

public class ClanMapping : IEntityTypeConfiguration<Clan>
{
    public void Configure(EntityTypeBuilder<Clan> builder)
    {
        builder.ToTable("clan");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);

        builder
        .HasIndex(p => p.Tag)
        .IsUnique();

        builder.HasMany(g => g.Membros)
            .WithOne()
            .HasForeignKey(g => g.ClanId);
    }
}
