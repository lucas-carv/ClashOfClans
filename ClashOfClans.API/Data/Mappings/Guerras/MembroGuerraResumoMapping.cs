using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class MembroGuerraResumoMapping : IEntityTypeConfiguration<MembroGuerraResumo>
{
    public void Configure(EntityTypeBuilder<MembroGuerraResumo> builder)
    {
        builder.HasIndex(x => new { x.ClanTag, x.Tag })
               .IsUnique();
    }
}