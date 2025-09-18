using System.ComponentModel.DataAnnotations;

namespace ClashOfClans.API.InputModels
{
    public class ClanInputModel
    {
        [Required(ErrorMessage = "A Tag do clan é obrigatória")]
        public required string Tag { get; set; }
        [Required(ErrorMessage = "O nome do clan é obrigatório")]
        public required string Nome { get; set; }
        public List<MembroDTO> Membros { get; set; } = [];
    }
    public class MembroDTO
    {
        [Required(ErrorMessage = "A Tag do membro é obrigatória")]
        public required string Tag { get; set; }
        [Required(ErrorMessage = "O nome do membro é obrigatório")]
        public required string Nome { get; set; }
    }
}
