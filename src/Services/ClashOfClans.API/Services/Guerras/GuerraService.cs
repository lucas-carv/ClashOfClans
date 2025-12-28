using ClashOfClans.API.DTOs.Guerras;
using ClashOfClans.API.Model.Guerras;

namespace ClashOfClans.API.Services.Guerras;

public class GuerraService
{
    public Guerra CriarGuerra(string status, DateTime inicioGuerra, DateTime fimGuerra, string tipoGuerra, ClanEmGuerraDTO clan)
    {
        ClanEmGuerra clanEmGuerra = new(clan.Tag, clan.Nome);
        foreach (var membro in clan.Membros)
        {
            MembroEmGuerra membroEmGuerra = clanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome, membro.PosicaoMapa);
            foreach (var ataque in membro.Ataques)
            {
                membroEmGuerra.AdicionarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas, ataque.PercentualDestruicao);
            }
        }
        Guerra novaGuerra = new(status, inicioGuerra, fimGuerra, tipoGuerra, clanEmGuerra);
        return novaGuerra;
    }
    public Guerra AtualizarGuerra(Guerra guerraExistente, string status, DateTime fimGuerra, ClanEmGuerraDTO clan)
    {
        guerraExistente.Status = status;
        if (!guerraExistente.FimGuerra.Equals(fimGuerra))
        {
            guerraExistente.AlterarDataFinalGuerra(fimGuerra);
        }
        foreach (var membro in clan.Membros)
        {
            MembroEmGuerra membroEmGuerra = guerraExistente.ClanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome, membro.PosicaoMapa);

            foreach (var ataque in membro.Ataques)
            {
                membroEmGuerra.AdicionarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas, ataque.PercentualDestruicao);
            }
        }
        return guerraExistente;
    }
}
