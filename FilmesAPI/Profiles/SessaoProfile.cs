using AutoMapper;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles {
    public class SessaoProfile : Profile {
        public SessaoProfile() {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>();
            CreateMap<UpdateSessaoDto, Sessao>();
        }
    }
}
