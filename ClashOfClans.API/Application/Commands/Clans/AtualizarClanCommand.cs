using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels;
using ClashOfClans.API.Model;
using ClashOfClans.API.Repositories;
using MediatR;

namespace ClashOfClans.API.Application.Commands.Clans;

public class AtualizarClanCommandHandler(IClanRepository clanRepository) : CommandHandler, IRequestHandler<AtualizarClanCommand, CommandResponse<bool>>
{
    private readonly IClanRepository _clanRepository = clanRepository;

    public async Task<CommandResponse<bool>> Handle(AtualizarClanCommand request, CancellationToken cancellationToken)
    {
        Clan clan = await _clanRepository.ObterClanPorTag(request.Tag);
        if (clan is null)
        {
            AdicionarErro($"Clan com a tag {request.Tag} não encontrado para ser atualizado");
            return new CommandResponse<bool>(ValidationResult);
        }
        var membrosParaAdicionar = request.Membros
            .Where(me => !clan.Membros.Any(m => m.Tag == me.Tag && m.Situacao == SituacaoMembro.Ativo))
            .Select(membroDTO => membroDTO);

        foreach (var membro in membrosParaAdicionar)
        {
            clan.AdicionarMembro(membro.Tag, membro.Nome);
        }

        IEnumerable<string> membrosTagParaInativar = clan.Membros
            .Where(m => !request.Membros.Any(me => me.Tag == m.Tag))
            .Select(m => m.Tag);

        foreach (var membroTag in membrosTagParaInativar)
        {
            clan.InativarMembro(membroTag);
        }

        _clanRepository.Update(clan);

        ValidationResult = await PersistirDados(_clanRepository.UnitOfWork);

        var result = new CommandResponse<bool>(ValidationResult);
        return result;
    }
}

public record AtualizarClanCommand(string Tag, string Nome, List<MembroDTO> Membros) : Command<CommandResponse<bool>>
{
    public string Tag { get; private set; } = Tag;
    public string Nome { get; private set; } = Nome;
    public List<MembroDTO> Membros { get; private set; } = Membros;
}
