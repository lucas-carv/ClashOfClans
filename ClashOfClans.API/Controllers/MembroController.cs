using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers
{
    [Route("api/v1/membro")]
    public class MembroController( ClashOfClansContext context) : ControllerBase
    {
        [HttpGet("clanTag/{clanTag}")]
        public async Task<IActionResult> ObterMembrosPorClanTag(string clanTag)
        {
            var membros = await context.MembrosGuerrasResumo
                .Where(m => m.ClanTag == clanTag &&
                       context.Membros.Any(mem => mem.Tag == m.Tag && mem.Situacao == SituacaoMembro.Ativo)).OrderBy(m => m.QuantidadeAtaques).ThenByDescending(m => m.GuerrasParticipadasSeq)
                .ToListAsync();

            List<MembroViewModel> membrosViewModel = membros.Select(m => new MembroViewModel
            {
                Nome = m.Nome,
                Tag = m.Tag,
                QuantidadeAtaques = m.QuantidadeAtaques,
                GuerrasParticipadasSeq = m.GuerrasParticipadasSeq
            }).ToList();

            return Ok(membrosViewModel);
        }
    }

    public class MembroViewModel
    {
        public string Tag { get; set; }
        public string Nome { get; set; }
        public int GuerrasParticipadasSeq { get; set; }
        public int QuantidadeAtaques { get; set; }
    }
}
