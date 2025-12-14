using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.API.Data.Mappings.Guerras;

public class MembroGuerraMapping : IEntityTypeConfiguration<MembroEmGuerra>
{
    public void Configure(EntityTypeBuilder<MembroEmGuerra> builder)
    {
        builder.ToTable("membro_em_guerra");
        builder.HasQueryFilter(p => p.FoiRemovido != null);
        builder.HasKey(c => c.Id);
    }
}
