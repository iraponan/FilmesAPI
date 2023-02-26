using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Filme
{
    public class UpdateFilmeDto
    {

        [Required(ErrorMessage = "O título do filme é obrigatorio!")]
        [StringLength(30)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres.")]
        public string Genero { get; set; }

        [Required]
        [Range(1, 300, ErrorMessage = "A duração deve ser entre 1 e 300 minutos.")]
        public int Duracao { get; set; }
    }
}
