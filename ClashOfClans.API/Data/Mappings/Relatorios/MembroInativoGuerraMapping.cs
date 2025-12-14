using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Relatorios;

public class MembroInativoGuerraMapping : IEntityTypeConfiguration<MembroInativoGuerra>
{
    public void Configure(EntityTypeBuilder<MembroInativoGuerra> builder)
    {
        builder.ToTable("membro_inativo_guerra");
        builder.HasKey(m => new { m.MembroTag, m.ClanTag });
    }
}