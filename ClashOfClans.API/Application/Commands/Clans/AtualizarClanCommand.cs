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
            .Where(ia => !clan.Membros.Any(ic => ic.Tag == ia.Tag && ic.Situacao == SituacaoMembro.Ativo))
            .Select(i => i).ToList()!;

        foreach (var membro in membrosParaAdicionar)
        {
            clan.AdicionarMembro(membro.Tag, membro.Nome);
        }

        IEnumerable<string> membrosTagParaInativar = clan.Membros
            .Where(ia => !request.Membros.Any(ic => ic.Tag == ia.Tag))
            .Select(i => i.Tag)!;

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

public class AtualizarClanCommand(string tag, string nome, List<MembroDTO> membros) : Command<CommandResponse<bool>>
{
    public string Tag { get; private set; } = tag;
    public string Nome { get; private set; } = nome;
    public List<MembroDTO> Membros { get; private set; } = membros;
}
