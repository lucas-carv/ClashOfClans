using ClashOfClans.Integracao.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClashOfClans.Integracao.API.Data.Mappings
{
    public class HistoricoMapping : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            builder.ToTable("historico_integracao");
            builder.HasKey(c => new { c.NomeRecurso });
        }
    }
}
