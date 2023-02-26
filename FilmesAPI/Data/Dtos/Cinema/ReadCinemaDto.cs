﻿using FilmesAPI.Data.Dtos.Endereco;

namespace FilmesAPI.Data.Dtos.Cinema
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDto ReadEnderecoDto { get; set; }
    }
}
