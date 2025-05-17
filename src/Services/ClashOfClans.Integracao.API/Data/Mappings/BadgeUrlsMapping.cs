using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClashOfClans.Integracao.API.Model.Guerras;

public class BadgeUrlsMapping : IEntityTypeConfiguration<BadgeUrls>
{
    public void Configure(EntityTypeBuilder<BadgeUrls> builder)
    {
        builder.ToTable("badge_urls");
        builder.HasKey(b => b.Id);

        // Define a relação com Clan (FK ClanTag)
        builder.HasOne(b => b.Clan)      // Referencia o objeto de navegação
               .WithOne(c => c.BadgeUrls) // Navegação inversa
               .HasForeignKey<BadgeUrls>(b => b.ClanTag) // Liga o ClanTag à Tag de Clan
               .HasPrincipalKey<Clan>(c => c.Tag); // Define o Tag como chave principal

        // Define a relação com Clan (FK ClanTag)
        builder.HasOne(b => b.Opponent)      // Referencia o objeto de navegação
               .WithOne(c => c.BadgeUrls) 
               .HasForeignKey<BadgeUrls>(b => b.ClanTag) 
               .HasPrincipalKey<Opponent>(c => c.Tag); 
    }
}