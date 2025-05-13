using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.Data;
using ClashOfClans.Integracao.API.Model.Guerras;
using ClashOfClans.Integracao.API.Repositories;
using MediatR;

namespace ClashOfClans.Integracao.API.Application.Commands.Guerras
{
    public class AdicionarGuerrasCommandHandler : CommandHandler, IRequestHandler<AdicionarGuerrasCommand, CommandResponse<bool>>
    {
        private readonly IGuerrasRepository _guerrasRepository;

        public AdicionarGuerrasCommandHandler(ClashOfClansContext context, IGuerrasRepository guerrasRepository) : base(context)
        {
            _guerrasRepository = guerrasRepository;
        }

        public async Task<CommandResponse<bool>> Handle(AdicionarGuerrasCommand command, CancellationToken cancellationToken)
        {
            if (!(command.EhValido()))
            {
                return new CommandResponse<bool>(command.ValidationResult);
            }

            foreach (ItemInputModel item in command.Items)
            {
                var existeGuerra = await _guerrasRepository.ObterGuerraPorData(item.EndTime);
                if (existeGuerra)
                    continue;

                Guerra guerra = new(item.Result, item.EndTime, item.TeamSize, item.AttacksPerMember, item.BattleModifier, item.Clan, item.Opponent);

                await _guerrasRepository.Add(guerra);
            }

            bool validation = await PersistirDados();
            return new CommandResponse<bool>(validation);
        }
    }
}

