using AutoMapper;
using Seventh_api.Dtos.Servidores;
using Seventh_api.Models;

namespace Seventh_api.Profiles;

public class ServidorProfile : Profile
{
    public ServidorProfile()
    {
        CreateMap<CreateServidorDto, Servidor>();
        CreateMap<UpdateServidorDto, Servidor>();
        CreateMap<ReadServidorDto, Servidor>();
        CreateMap<Servidor, UpdateServidorDto>();
        CreateMap<Servidor, ReadServidorDto>();
        CreateMap<Servidor, CreateServidorDto>();
    }
}