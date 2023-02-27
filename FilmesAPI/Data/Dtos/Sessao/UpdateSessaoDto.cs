using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Sessao {
    public class UpdateSessaoDto {
        [Required]
        public DateTime DataEHoraDaSessao { get; set; }

        [Required]
        public int FilmeId { get; set; }
    }
}
