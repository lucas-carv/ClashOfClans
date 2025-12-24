using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Relatorios;

public class MembroGuerraResumoMapping : IEntityTypeConfiguration<MembroGuerraResumo>
{
    public void Configure(EntityTypeBuilder<MembroGuerraResumo> builder)
    {
        builder.HasKey(m => new { m.MembroTag, m.ClanTag });
    }
}