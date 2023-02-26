using AutoMapper;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles {
    public class EnderecoProfile : Profile {
        protected EnderecoProfile() {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, ReadEnderecoDto>();
            CreateMap<UpdateEnderecoDto, Endereco>();
        }
    }
}
