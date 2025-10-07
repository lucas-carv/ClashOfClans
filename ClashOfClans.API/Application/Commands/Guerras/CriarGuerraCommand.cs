using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System.Security.Policy;

namespace ClashOfClans.API.Application.Commands.Guerras;
public record CriarGuerraRequest(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanGuerra Clan) : IRequest<CommandResult<bool>>;

public class CriarGuerraCommandHandler(IGuerraRepository guerraRepository, IClanRepository clanRepository) : CommandHandler, IRequestHandler<CriarGuerraRequest, CommandResult<bool>>
{
    private readonly IGuerraRepository _guerraRepository = guerraRepository;
    private readonly IClanRepository _clanRepository = clanRepository;

    public async Task<CommandResult<bool>> Handle(CriarGuerraRequest request, CancellationToken cancellationToken)
    {
        bool existeClan = await _clanRepository.VerificarSeExisteClan(request.Clan.Tag);
        if (!existeClan)
        {
            var erro = new ErrorMessage("teste", "teste");
            var erros = new List<ErrorMessage>();
            erros.Add(erro);
            return CommandResult<bool>.InvalidInput(erros);
        }

        List<MembroGuerra> participantes = new();
        GuerraClan guerraClan = new();

        Maybe<Guerra> guerra = await _guerraRepository.ObterGuerraPorDatas(request.InicioGuerra, request.FimGuerra);
        Guerra novaGuerra = new();
        if (guerra.HasNoValue)
        {
            novaGuerra = new(request.Status, request.InicioGuerra, request.FimGuerra, guerraClan); ;
        }
        else
        {
            novaGuerra = guerra.Value;
        }

        foreach (var membros in request.Clan.Membros)
        {
            List<Ataque> ataques = membros.Ataques.Select(m => new Ataque()
            {
                Estrelas = m.Estrelas,
            }).ToList();

            guerraClan.AdicionarMembro(membros.Tag, membros.Nome, ataques);
        }


        _guerraRepository.Add(novaGuerra);

        ValidationResult = await PersistirDados(_guerraRepository.UnitOfWork);

        var result = new CommandResponse<bool>(ValidationResult, ValidationResult.IsValid);
        return new CommandResult<bool>();
    }
}



public record ClanGuerra
{
    public string Tag { get; set; } = string.Empty;
    public List<ParticipantesGuerra> Membros { get; set; } = [];
}
public record ParticipantesGuerra
{
    public string Tag { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public List<Ataques> Ataques { get; set; } = [];

}
public class Ataques
{
    public int Estrelas { get; set; }
}