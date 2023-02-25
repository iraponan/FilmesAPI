﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models {
    public class Filme {
        [Required(ErrorMessage = "O título do filme é obrigatorio!")]
        [StringLength(30)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O gênero do filme é obrigatorio!")]
        [MaxLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres.")]
        public string Genero { get; set; }
        [Required]
        [Range(1, 120, ErrorMessage = "A duração deve ser entre 70 e 600 minutos.")]
        public int Duracao { get; set; }
    }
}
