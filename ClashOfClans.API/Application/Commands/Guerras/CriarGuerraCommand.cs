using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels.Guerras;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Repositories;
using MediatR;

namespace ClashOfClans.API.Application.Commands.Guerras;

public class CriarGuerraCommandHandler(IGuerraRepository guerraRepository, IClanRepository clanRepository) : CommandHandler, IRequestHandler<CriarGuerraCommand, CommandResponse<bool>>
{
    private readonly IGuerraRepository _guerraRepository = guerraRepository;
    private readonly IClanRepository _clanRepository = clanRepository;

    public async Task<CommandResponse<bool>> Handle(CriarGuerraCommand command, CancellationToken cancellationToken)
    {
        bool existeClan = await _clanRepository.VerificarSeExisteClan(command.Clan.Tag);

        if (!existeClan)
        {
            AdicionarErro($"O clan informado com tag {command.Clan.Tag} não existe na base de dados");
            return new CommandResponse<bool>(ValidationResult);
        }

        List<MembroGuerra> participantes = new();
        GuerraClan guerraClan = new()


        Guerra guerra = await _guerraRepository.ObterGuerraPorDatas(command.InicioGuerra, command.FimGuerra);
        if (guerra is null)
        {
             guerra = new(command.Status, command.InicioGuerra, command.FimGuerra, guerraClan); ;
        }

        foreach (var membros in command.Clan.Membros)
        {
            List<Ataque> ataques = membros.Ataques.Select(m => new Ataque()
            {
                Estrelas = m.Estrelas,
            }).ToList();

            guerraClan.AdicionarMembro(membros.Tag, membros.Nome, ataques);
        }

        

        _guerraRepository.Add(guerra);

        ValidationResult = await PersistirDados(_guerraRepository.UnitOfWork);

        var result = new CommandResponse<bool>(ValidationResult, ValidationResult.IsValid);
        return result;
    }
}


public class CriarGuerraCommand : Command<CommandResponse<bool>>
{
    public string Status { get; private set; }
    public DateTime InicioGuerra { get; private set; }
    public DateTime FimGuerra { get; private set; }
    public ClanGuerraDTO Clan { get; private set; } = new();

    public CriarGuerraCommand(string status, DateTime inicioGuerra, DateTime fimGuerra, ClanGuerraDTO clan)
    {
        Status = status;
        InicioGuerra = inicioGuerra;
        FimGuerra = fimGuerra;
        Clan = clan;
    }
}