﻿using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles {
    public class CinemaProfile : Profile {
        public CinemaProfile() {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>().ForMember(cinemaDto => cinemaDto.Endereco, 
                options => options.MapFrom(cinema => cinema.Endereco));
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
