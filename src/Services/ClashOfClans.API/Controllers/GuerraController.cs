using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediator mediator, ClashOfClansContext context) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Obtém guerras por tag do clan
    /// </summary>
    /// <response code="200">Retorna os guerras pela tag do clan</response>
    [ProducesResponseType(typeof(List<GuerraViewModel>), StatusCodes.Status200OK)]
    [HttpGet("clanTag/{clanTag}")]
    public async Task<IActionResult> ObterGuerras(string clanTag)
    {
        List<GuerraViewModel> guerras = await context.Guerras
            .Where(g =>
                g.ClansEmGuerra.Any(c => c.Tag == clanTag &&
                g.TipoGuerra.Equals("Normal")))
            .OrderByDescending(g => g.FimGuerra)
            .Select(g => new GuerraViewModel
            {
                FimGuerra = g.FimGuerra,
                InicioGuerra = g.InicioGuerra,
                Status = g.Status,
                TipoGuerra = g.TipoGuerra
            })
            .ToListAsync();

        return Ok(guerras);
    }


    /// <summary>
    /// Obtém logs de guerras por tag do clan
    /// </summary>
    /// <response code="200">Retorna os logs de guerras pela tag do clan</response>
    [ProducesResponseType(typeof(IEnumerable<LogGuerraViewModel>), StatusCodes.Status200OK)]
    [HttpGet("log/clanTag/{clanTag}")]
    public async Task<IActionResult> ObterLogs(string clanTag)
    {
        // necessário fazer query para popular as tabelas de guerras com os dados de estrelas dos clans
        var guerras = await context.Guerras
          .Where(g => g.ClansEmGuerra.Count.Equals(2))
          .Include(g => g.ClansEmGuerra)
          .Where(g => g.ClansEmGuerra.Select(c => c.Tag).Contains(clanTag))
          .ToListAsync();

        var result = guerras
            .Select(x => new
            {
                Principal = x.ClansEmGuerra
                    .Where(c => c.Tipo.Equals(TipoClanNaGuerra.Principal))
                    .Select(c => new
                    {
                        c.Nome,
                        c.Tag,
                        c.Estrelas
                    })
                    .FirstOrDefault(),

                Oponente = x.ClansEmGuerra
                    .Where(c => c.Tipo.Equals(TipoClanNaGuerra.Oponente))
                    .Select(c => new
                    {
                        c.Nome,
                        c.Tag,
                        c.Estrelas
                    })
                    .FirstOrDefault(),

                Resultado = ObterResultado(x),
                x.InicioGuerra,
                x.FimGuerra
            }).OrderByDescending(f => f.FimGuerra).ToList();


        IEnumerable<LogGuerraViewModel> resultado = result.Select(l => new LogGuerraViewModel
        {
            ClanNome = l.Principal.Nome,
            EstrelasClan = l.Principal.Estrelas,
            OponenteNome = l.Oponente.Nome,
            EstrelasOponente = l.Oponente.Estrelas,
            Resultado = l.Resultado,
            FimGuerra = l.FimGuerra,
            InicioGuerra = l.InicioGuerra,
            ClanTag = l.Principal.Tag,
            OponenteTag = l.Oponente.Tag
        });
        return Ok(resultado);
    }


    [HttpGet("detalhe/clan-tag/{clanTag}/oponente-tag/{oponenteTag}")]
    public async Task<IActionResult> ObterDetalhamentoGuerraPorClanTags(string clanTag, string oponenteTag)
    {
        var guerra = await context.Guerras
            .Where(g =>
                g.ClansEmGuerra.Any(c => c.Tag == clanTag) &&
                g.ClansEmGuerra.Any(c => c.Tag == oponenteTag))
            .Include(g => g.ClansEmGuerra)
            .ThenInclude(c => c.MembrosEmGuerra.OrderBy(m => m.PosicaoMapa))
            .ThenInclude(m => m.Ataques)
            .FirstAsync();


        var membros = guerra.ClansEmGuerra.SelectMany(c => c.MembrosEmGuerra);

        var model = new DetalhamentoGuerraViewModel
        {
            InicioGuerra = guerra.InicioGuerra,
            FimGuerra = guerra.FimGuerra,
            Status = guerra.Status,
            ClansEmGuerra = guerra.ClansEmGuerra.Select(c => new ClanEmGuerraViewModel
            {
                Nome = c.Nome,
                Tag = c.Tag,
                Tipo = c.Tipo,
                MembrosEmGuerra = c.MembrosEmGuerra.Select(m => new MembroEmGuerraViewModel
                {
                    CentroVilaLevel = m.CentroVilaLevel,
                    Nome = m.Nome,
                    Tag = m.Tag,
                    PosicaoMapa = m.PosicaoMapa,
                    Ataques = m.Ataques.Select(a => new GuerraMembroAtaqueViewModel
                    {
                        AtacanteTag = a.AtacanteTag,
                        DefensorTag = a.DefensorTag,
                        Estrelas = a.Estrelas,
                        PercentualDestruicao = a.PercentualDestruicao,
                        NomeDefensor = membros.Where(m => m.Tag == a.DefensorTag).First().Nome,
                        PosicaoMapa = membros.Where(m => m.Tag == a.DefensorTag).First().PosicaoMapa
                    }).OrderBy(m => m.PosicaoMapa).ToList()
                })
                .OrderBy(m => m.PosicaoMapa)
                .ToList()
            }).ToList()
        };

        return Ok(model);
    }


    /// <summary>
    /// Cria ou atualiza uma guerra
    /// </summary>
    /// <response code="200">Cria ou atualiza uma guerra e retorna o response</response>
    [ProducesResponseType(typeof(UpsertGuerraResponse), StatusCodes.Status200OK)]
    [HttpPut("criar")]
    public async Task<IActionResult> UpsertGuerra([FromBody] UpsertGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }

    //mover isso para o QueryHandler quando criar o processo de CQRS para Queries
    private static string ObterResultado(Guerra guerra)
    {
        if (guerra.Status.Equals("preparation", StringComparison.OrdinalIgnoreCase))
            return "preparation";

        if (guerra.Status.Equals("inWar", StringComparison.OrdinalIgnoreCase))
            return "inWar";

        ClanEmGuerra? principal = guerra.ClansEmGuerra.FirstOrDefault(c => c.Tipo == TipoClanNaGuerra.Principal);
        ClanEmGuerra? oponente = guerra.ClansEmGuerra.FirstOrDefault(c => c.Tipo == TipoClanNaGuerra.Oponente);
        if (principal is null || oponente is null)
            return "unknown";

        return principal.Estrelas switch
        {
            var estrelas when estrelas > oponente.Estrelas => "win",
            var estrelas when estrelas < oponente.Estrelas => "lose",
            _ => "draw"
        };
    }
}

public class DetalhamentoGuerraViewModel
{
    public string Status { get; set; }
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public List<ClanEmGuerraViewModel> ClansEmGuerra { get; set; } = [];
    public string TipoGuerra { get; init; }

}
public class ClanEmGuerraViewModel
{
    public string Tag { get; init; }
    public string Nome { get; set; }
    public List<MembroEmGuerraViewModel> MembrosEmGuerra { get; set; } = [];
    public TipoClanNaGuerra Tipo { get; set; }
}

public class MembroEmGuerraViewModel
{
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int PosicaoMapa { get; set; }
    public int CentroVilaLevel { get; set; }
    public List<GuerraMembroAtaqueViewModel> Ataques { get; set; } = [];
}

public class GuerraMembroAtaqueViewModel
{
    public string AtacanteTag { get; set; }
    public string DefensorTag { get; set; }
    public string NomeDefensor { get; set; }
    public int Estrelas { get; set; }
    public int PosicaoMapa { get; set; }
    public decimal PercentualDestruicao { get; set; }
}