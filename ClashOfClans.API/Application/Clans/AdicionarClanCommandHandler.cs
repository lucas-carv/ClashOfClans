using ClashOfClans.API.Core;
using ClashOfClans.API.Model;
using ClashOfClans.API.Repositories;
using MediatR;

namespace ClashOfClans.API.Application.Clans;

public class AdicionarClanCommandHandler : CommandHandler, IRequestHandler<AdicionarClanCommand, CommandResponse<bool>>
{
    private readonly IClanRepository _clanRepository;

    public AdicionarClanCommandHandler(IClanRepository clanRepository)
    {
        _clanRepository = clanRepository;
    }
    public async Task<CommandResponse<bool>> Handle(AdicionarClanCommand request, CancellationToken cancellationToken)
    {
        Clan clan = new(request.Tag, request.Nome);

        foreach (var membros in request.MemberList)
        {
            Membro membro = new()
            {
                Nome = membros.Name,
                Tag = membros.Tag
                //TownHallLevel = membros.TownHallLevel
            };
            clan.AdicionarMembro(membro);
        }

        _clanRepository.Add(clan);
        var validation = await PersistirDados(_clanRepository.UnitOfWork);

        var result = new CommandResponse<bool>(ValidationResult, ValidationResult.IsValid);

        return result;
    }
}
