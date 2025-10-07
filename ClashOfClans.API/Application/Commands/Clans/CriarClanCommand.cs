using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels;
using ClashOfClans.API.Model;
using ClashOfClans.API.Repositories;
using MediatR;

namespace ClashOfClans.API.Application.Commands.Clans;

public class CriarClanCommandHandler(IClanRepository clanRepository) : CommandHandler, IRequestHandler<CriarClanCommand, CommandResponse<bool>>
{
    private readonly IClanRepository _clanRepository = clanRepository;

    public async Task<CommandResponse<bool>> Handle(CriarClanCommand command, CancellationToken cancellationToken)
    {
        bool existeClan = await _clanRepository.VerificarSeExisteClan(command.Tag);
        if (existeClan)
        {
            AdicionarErro($"O Clan de tag {command.Tag} já existe");
            return new CommandResponse<bool>(ValidationResult);
        }

        Clan clan = new(command.Tag, command.Nome);

        foreach (var membro in command.Membros)
        {
            clan.AdicionarMembro(membro.Tag, membro.Nome);
        }

        _clanRepository.Add(clan);

        ValidationResult = await PersistirDados(_clanRepository.UnitOfWork);

        var result = new CommandResponse<bool>(ValidationResult, ValidationResult.IsValid);
        return result;
    }
}

public record CriarClanCommand(string tag, string nome, List<MembroDTO> membros) : Command<CommandResponse<bool>>
{
    public string Tag { get; private set; } = tag;
    public string Nome { get; private set; } = nome;
    public List<MembroDTO> Membros { get; private set; } = membros;

    //public string Type { get; set; } = string.Empty;
    //public string Description { get; set; } = string.Empty;
    //public bool IsFamilyFriendly { get; set; }
    //public int ClanLevel { get; set; }
    //public int ClanPoints { get; set; }
    //public int ClanBuilderBasePoints { get; set; }
    //public int ClanCapitalPoints { get; set; }
    //public int RequiredTrophies { get; set; }
    //public string WarFrequency { get; set; } = string.Empty;
    //public int WarWinStreak { get; set; }
    //public int WarWins { get; set; }
    //public int WarTies { get; set; }
    //public int WarLosses { get; set; }
    //public bool IsWarLogPublic { get; set; }
    //public int Members { get; set; }
    //public int RequiredBuilderBaseTrophies { get; set; }
    //public int RequiredTownhallLevel { get; set; }

}
