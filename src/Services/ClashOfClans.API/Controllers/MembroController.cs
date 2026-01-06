using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Clans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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
                        context.Membros
                        .Any(mem => mem.Tag == m.MembroTag &&
                             mem.Situacao == SituacaoMembro.Ativo
                             && m.QuantidadeAtaques <= 2))
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


        [HttpGet("clanTag/{clanTag}/desempenho")]
        public async Task<IActionResult> ObterDesempenhoDeMembros(string clanTag, int minimoGuerras, int maximoGuerras)
        {
            var ultimasGuerrasIds = await context.Guerras
                .AsNoTracking()
                .Where(g => g.ClanEmGuerra.Tag == clanTag && g.Status == "WarEnded")
                .OrderByDescending(g => g.DataCriacao)
                .Take(5)
                .Select(g => g.Id)
                .ToListAsync();

            var membrosDaGuerra = await context.MembrosEmGuerra
                .AsNoTracking()
                .Join(context.ClansEmGuerra,
                    m => m.ClanEmGuerraId,
                    c => c.Id,
                    (m, c) => new { Membro = m, Clan = c })
                .Join(context.Guerras,
                    mc => mc.Clan.GuerraId,
                    g => g.Id,
                    (mc, g) => new { mc.Membro, mc.Clan, Guerra = g })
                .Where(x => ultimasGuerrasIds.Contains(x.Guerra.Id))
                .Select(x => new
                {
                    GuerraId = x.Guerra.Id,
                    MembroTag = x.Membro.Tag,
                    Nome = x.Membro.Nome,
                    QuantidadeEstrelas = x.Membro.Ataques.Sum(p => p.Estrelas),
                    TotalDestruicao = x.Membro.Ataques.Sum(a => a.PercentualDestruicao)
                })
                .ToListAsync();


            var guerras = await context.Guerras
                 .AsNoTracking()
                 .Where(g => ultimasGuerrasIds.Contains(g.Id))
                 .Select(g => new
                 {
                     g.Id,
                     Membros = g.ClanEmGuerra.MembrosEmGuerra
                         .Select(m => new
                         {
                             m.Id,
                             m.Tag,
                             m.Nome
                         })
                         .ToList()
                 })
                 .ToListAsync();

            var membroIds = guerras
                .SelectMany(g => g.Membros)
                .Select(m => m.Id)
                .Distinct()
                .ToList();

            var ataques = await context.GuerraMembroAtaques
                .AsNoTracking()
                .Where(a => membroIds.Contains(a.MembroEmGuerraId))
                .Select(a => new
                {
                    a.MembroEmGuerraId,
                    a.Estrelas,
                    a.PercentualDestruicao
                })
                .ToListAsync();

            var dados =
                 from g in guerras
                 from m in g.Membros
                 join a in ataques on m.Id equals a.MembroEmGuerraId
                 select new
                 {
                     GuerraId = g.Id,
                     m.Tag,
                     m.Nome,
                     a.Estrelas,
                     a.PercentualDestruicao
                 };

            var resultado = dados
                .GroupBy(x => new { x.Tag, x.Nome })
                .Select(g => new
                {
                    g.Key.Tag,
                    g.Key.Nome,
                    TotalAtaques = g.Count(),              // máx 10
                    TotalEstrelas = g.Sum(x => x.Estrelas),
                    MediaEstrelas = g.Average(x => x.Estrelas),
                    MediaDestruicao = g.Average(x => x.PercentualDestruicao),
                    QuantidadeGuerras = g
                        .Select(x => x.GuerraId)
                        .Distinct()
                        .Count()
                })
                .ToList();


            var desempenho = resultado
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
                }).ToList();
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
        public decimal MediaDestruicao { get; set; }
        public int QuantidadeGuerras { get; set; }
    }
}
