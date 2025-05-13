using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.Model.Guerras;
using FluentValidation;

namespace ClashOfClans.Integracao.API.Application.Commands.Guerras
{
    public class AdicionarGuerrasCommand : Command<CommandResponse<bool>>
    {
        public List<ItemInputModel> Items { get; set; } = [];

        public AdicionarGuerrasCommand(List<ItemInputModel> items)
        {
            Items = items;
        }
        public override bool EhValido()
        {
            ValidationResult = new AdicionarGuerrasCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarGuerrasCommandValidation : AbstractValidator<AdicionarGuerrasCommand>
    {
        public AdicionarGuerrasCommandValidation()
        {
            RuleFor(c => c.Items)
               .NotEmpty()
               .WithMessage("Nenhuma guerra enviada.");
        }
    }

    public class ItemInputModel
    {
        public string Result { get; set; } = string.Empty;
        public DateTime EndTime { get; set; } = new DateTime();
        public int TeamSize { get; set; }
        public int AttacksPerMember { get; set; }
        public string BattleModifier { get; set; } = string.Empty;
        public Clan Clan { get; set; } = new Clan();
        public Opponent Opponent { get; set; } = new Opponent();
    }

    public class ClanInputModel
    {
        public string Tag { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public BadgeUrlsInputModel BadgeUrls { get; set; } = new();
        public int ClanLevel { get; set; }
        public int Attacks { get; set; }
        public int Stars { get; set; }
        public double DestructionPercentage { get; set; }
        public int ExpEarned { get; set; }
    }

    public class OpponentInputModel
    {
        public string Tag { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public BadgeUrlsInputModel BadgeUrls { get; set; } = new();
        public int ClanLevel { get; set; }
        public int Stars { get; set; }
        public double DestructionPercentage { get; set; }
    }

    public class BadgeUrlsInputModel
    {
        public string Small { get; set; } = string.Empty;
        public string Large { get; set; } = string.Empty;
        public string Medium { get; set; } = string.Empty;
    }
}

