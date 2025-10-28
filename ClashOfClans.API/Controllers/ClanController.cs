using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels;
using ClashOfClans.API.Model;
using ClashOfClans.API.Repositories;
using ClashOfClans.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/clan")]
public class ClanController(IMediator mediator, IClanRepository clanRepository) : MainController
{
    private readonly IMediator _mediator = mediator;
    private readonly IClanRepository _clanRepository = clanRepository;

    [HttpGet("{tag}")]
    public async Task<ActionResult<ClanViewModel>> ObterPorTag(string tag)
    {
        Clan clan = await _clanRepository.ObterClanPorTag(tag);
        if (clan is null)
        {
            return RegistroNaoEncontrado($"Clan não encontrado");
        }
        ClanViewModel clanViewModel = new()
        {
            Name = clan.Nome,
            Tag = clan.Tag
        };

        clanViewModel.MemberList.AddRange(clan.Membros.Select(m => new MemberViewModel()
        {
            Name = m.Nome,
            Tag = m.Tag
        }));

        return CustomResponse(clan);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> CriarClan([FromBody] CriarClanRequest inputModel)
    {
        var resultado = await _mediator.Send(inputModel);
        //if (!resultado.ValidationResult.IsValid)
        //{
        //    return CustomResponse(resultado.ValidationResult);
        //}
        return NoContent();
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> AtualizarClan([FromBody] CriarClanRequest inputModel)
    {
        AtualizarClanCommand adicionarClanCommand = new(inputModel.Tag, inputModel.Nome, inputModel.Membros);

        var resultado = await _mediator.Send(adicionarClanCommand);
        if (!resultado.ValidationResult.IsValid)
        {
            return CustomResponse(resultado.ValidationResult);
        }
        return NoContent();
    }
}
