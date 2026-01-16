using ClashOfClans.API.DTOs.Guerras;
using ClashOfClans.API.Model.Guerras;

namespace ClashOfClans.API.Services.Guerras;

public class GuerraService
{
    public Guerra CriarGuerra(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra, List<ClanEmGuerraDTO> clans)
    {
        Guerra novaGuerra = new(status, inicioGuerra, fimGuerra, tipoGuerra);

        foreach (var clan in clans)
        {
            var clanEmGuerra = new ClanEmGuerra(clan.Tag, clan.Nome, (TipoClanNaGuerra)clan.Tipo);
            foreach (var membro in clan.Membros)
            {
                MembroEmGuerra membroEmGuerra = clanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome, membro.PosicaoMapa);
                foreach (var ataque in membro.Ataques)
                {
                    membroEmGuerra.AdicionarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas, ataque.PercentualDestruicao);
                }
            }
            novaGuerra.AdicionarClan(clanEmGuerra);
        }
        return novaGuerra;
    }
    public Guerra AtualizarGuerra(Guerra guerraExistente, string status, DateTime fimGuerra, List<ClanEmGuerraDTO> clans)
    {
        guerraExistente.Status = status;
        if (!guerraExistente.FimGuerra.Equals(fimGuerra))
        {
            guerraExistente.AlterarDataFinalGuerra(fimGuerra);
        }

        foreach (var clan in clans)
        {
            ClanEmGuerra? clanGuerra = guerraExistente.ClansEmGuerra.FirstOrDefault(c => c.Tag.Equals(clan.Tag));
            if (clanGuerra is null)
            {
                clanGuerra = new ClanEmGuerra(clan.Tag, clan.Nome, (TipoClanNaGuerra)clan.Tipo);
                guerraExistente.AdicionarClan(clanGuerra);
            }

            foreach (var membro in clan.Membros)
            {
                MembroEmGuerra membroEmGuerra = clanGuerra!.AdicionarMembro(membro.Tag, membro.Nome, membro.PosicaoMapa);

                foreach (var ataque in membro.Ataques)
                {
                    membroEmGuerra.AdicionarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas, ataque.PercentualDestruicao);
                }
            }
        }
        return guerraExistente;
    }
}
