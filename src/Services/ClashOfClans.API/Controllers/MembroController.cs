using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Clans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers
{
    [Route("api/v1/membro")]
    public class MembroController(ClashOfClansContext context) : ControllerBase
    {
        [HttpGet("clanTag/{clanTag}")]
        public async Task<IActionResult> ObterMembrosPorClanTag(string clanTag)
        {
            var membros = await context.MembrosGuerrasResumo
                .Where(m => m.ClanTag == clanTag &&
                       context.Membros.Any(mem => mem.Tag == m.MembroTag && mem.Situacao == SituacaoMembro.Ativo)).OrderBy(m => m.QuantidadeAtaques).ThenByDescending(m => m.GuerrasParticipadasSeq)
                .ToListAsync();

            List<MembroViewModel> membrosViewModel = membros.Select(m => new MembroViewModel
            {
                Nome = m.Nome,
                Tag = m.MembroTag,
                QuantidadeAtaques = m.QuantidadeAtaques,
                GuerrasParticipadasSeq = m.GuerrasParticipadasSeq
            }).ToList();

            return Ok(membrosViewModel);
        }


        [HttpGet("clanTag/{clanTag}/desempenho")]
        public async Task<IActionResult> ObterDesempenhoDeMembros(string clanTag, int minimoGuerras = 0, int maximoGuerras = 10)
        {
            var ultimasGuerrasIds = await context.Guerras
                .AsNoTracking()
                .Where(g => g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.DataCriacao)
                .Take(maximoGuerras)
                .Select(g => g.Id)
                .ToListAsync();

            var query = context.GuerraMembroAtaques
                .AsNoTracking()
                .Where(x =>
                    ultimasGuerrasIds.Contains(
                        x.MembroEmGuerra.ClanEmGuerra.GuerraId
                    )
                )
                .GroupBy(x => new
                {
                    x.MembroEmGuerra.Tag,
                    x.MembroEmGuerra.Nome
                })
                .Select(g => new
                {
                    g.Key.Tag,
                    g.Key.Nome,
                    TotalAtaques = g.Count(),
                    TotalEstrelas = g.Sum(x => x.Estrelas),
                    MediaEstrelas = g.Sum(x => x.Estrelas) / (double)g.Count(),
                    MediaDestruicao = g.Sum(x => x.PercentualDestruicao) / g.Count(),
                    QuantidadeGuerras = g
                        .Select(x => x.MembroEmGuerra.ClanEmGuerra.GuerraId)
                        .Distinct()
                        .Count()
                }).Where(x =>
                    x.QuantidadeGuerras >= minimoGuerras &&
                    x.QuantidadeGuerras <= maximoGuerras
                );

            var desempenho = await query
                .OrderByDescending(x => x.MediaEstrelas)
                .Select(x => new DesempenhoMembroViewModel
                {
                    MembroTag = x.Tag,
                    Nome = x.Nome,
                    TotalAtaques = x.TotalAtaques,
                    TotalEstrelas = x.TotalEstrelas,
                    MediaEstrelas = Math.Round(x.MediaEstrelas, 2),
                    MediaDestruicao = Math.Round(x.MediaDestruicao, 2),
                    QuantidadeGuerras = x.QuantidadeGuerras
                })
                .ToListAsync();
            return Ok(desempenho);
        }
    }

    public class MembroViewModel
    {
        public string Tag { get; set; }
        public string Nome { get; set; }
        public int GuerrasParticipadasSeq { get; set; }
        public int QuantidadeAtaques { get; set; }
    }
    public class DesempenhoMembroViewModel
    {
        public string MembroTag { get; set; }
        public string Nome { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalEstrelas { get; set; }
        public double MediaEstrelas { get; set; }
        public decimal MediaDestruicao { get; set; }
        public int QuantidadeGuerras { get; set; }
    }
}
