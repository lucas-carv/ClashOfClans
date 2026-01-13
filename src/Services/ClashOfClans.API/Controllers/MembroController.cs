using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Clans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers
{
    [Route("api/v1/membro")]
    public class MembroController(ClashOfClansContext context) : ControllerBase
    {
        /// <summary>
        /// Obtém o resumo de ataque dos membros do clan
        /// </summary>
        /// <response code="200">Retorna o resumo de ataque dos membros</response>
        [ProducesResponseType(typeof(List<MembroViewModel>), StatusCodes.Status200OK)]
        [HttpGet("clanTag/{clanTag}/resumo")]
        public async Task<IActionResult> ObterResumoMembrosPorClanTag(string clanTag)
        {
            var membros = await context.MembrosGuerrasResumo
                    .Where(m => m.ClanTag == clanTag &&
                        context.Membros
                        .Any(mem => mem.Tag == m.MembroTag &&
                             mem.Situacao == SituacaoMembro.Ativo))
                    .OrderBy(m => m.QuantidadeAtaques)
                    .ThenByDescending(m => m.GuerrasParticipadasSeq)
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

        /// <summary>
        /// Obtém o desempenho dos membros
        /// </summary>
        /// <response code="200">Retorna o desempenho dos membros</response>
        [ProducesResponseType(typeof(List<DesempenhoMembroViewModel>), StatusCodes.Status200OK)]
        [HttpGet("clanTag/{clanTag}/desempenho")]
        public async Task<IActionResult> ObterDesempenhoDeMembros(string clanTag)
        {
            List<int> ultimasGuerrasIds = await context.Guerras
                .AsNoTracking()
                .Where(g => g.ClanEmGuerra.Tag == clanTag && g.Status == "WarEnded" && g.TipoGuerra == "Normal")
                .OrderByDescending(g => g.FimGuerra)
                .Take(5)
                .Select(g => g.Id)
                .ToListAsync();

            var membrosDaGuerra = await context.MembrosEmGuerra
                .AsNoTracking()
                .Join(context.ClansEmGuerra,
                    m => m.ClanEmGuerraId,
                    c => c.Id,
                    (m, c) => new { MembroEmGuerra = m, Clan = c })
                .Join(context.Guerras,
                    mc => mc.Clan.GuerraId,
                    g => g.Id,
                    (mc, g) => new { mc.MembroEmGuerra, mc.Clan, Guerra = g })
                .Join(context.Membros,
                    p => p.MembroEmGuerra.Tag,
                    membro => membro.Tag,
                    (p, membro) => new { p.MembroEmGuerra, p.Clan, p.Guerra, Membro = membro })
                .Where(x => ultimasGuerrasIds.Contains(x.Guerra.Id) && x.Membro.Situacao.Equals(SituacaoMembro.Ativo))
                .Select(x => new
                {
                    GuerraId = x.Guerra.Id,
                    MembroTag = x.MembroEmGuerra.Tag,
                    Nome = x.MembroEmGuerra.Nome,
                    QuantidadeEstrelas = x.MembroEmGuerra.Ataques.Sum(p => p.Estrelas),
                    TotalDestruicao = x.MembroEmGuerra.Ataques.Sum(a => a.PercentualDestruicao),
                    TotalAtaques = x.MembroEmGuerra.Ataques.Count
                })
                .ToListAsync();

            var membrosAgrupados = membrosDaGuerra.GroupBy(m => m.MembroTag);

            List<DesempenhoMembroViewModel> desempenho = membrosAgrupados.Select(x => new DesempenhoMembroViewModel()
            {
                MembroTag = x.Key,
                Nome = x.First().Nome,
                TotalAtaques = x.Sum(a => a.TotalAtaques),
                MediaDestruicao = Math.Round(x.Average(a => a.TotalAtaques == 0 ? 0 : (double)a.TotalDestruicao/ a.TotalAtaques), 2),
                MediaEstrelas = Math.Round(x.Average(a => a.TotalAtaques == 0 ? 0 : (double)a.QuantidadeEstrelas / a.TotalAtaques), 2),
                QuantidadeGuerras = x.Count(),
                TotalEstrelas = x.Sum(a => a.QuantidadeEstrelas)
            })
                .OrderByDescending(a => a.QuantidadeGuerras)
                .ThenByDescending(a => a.TotalAtaques)
                .ThenByDescending(a => a.MediaDestruicao)
                .ThenByDescending(a => a.MediaEstrelas)
                .ToList();

            return Ok(desempenho);
        }
    }

    public class MembroViewModel
    {
        public required string Tag { get; set; }
        public required string Nome { get; set; }
        public int GuerrasParticipadasSeq { get; set; }
        public int QuantidadeAtaques { get; set; }
    }
    public class DesempenhoMembroViewModel
    {
        public required string MembroTag { get; set; }
        public required string Nome { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalEstrelas { get; set; }
        public double MediaEstrelas { get; set; }
        public double MediaDestruicao { get; set; }
        public int QuantidadeGuerras { get; set; }
    }
}
