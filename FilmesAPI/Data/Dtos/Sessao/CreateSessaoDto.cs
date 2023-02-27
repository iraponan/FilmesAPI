using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Sessao {
    public class CreateSessaoDto {
        [Required]
        public DateTime DataEHoraDaSessao { get; set; }
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}
