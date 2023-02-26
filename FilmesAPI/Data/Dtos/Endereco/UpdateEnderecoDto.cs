﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Endereco
{
    public class UpdateEnderecoDto
    {
        [Required(ErrorMessage = "O campo de logradouro é obrigatório!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo de número é obrigatório!")]
        public int Numero { get; set; }
    }
}
