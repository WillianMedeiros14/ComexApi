using AutoMapper;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;

namespace ComexAPI.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<CreateClienteDto, Cliente>();
            CreateMap<Cliente, ReadClienteDto>()
                .ForMember(clienteDto => clienteDto.Endereco,
                    opt => opt.MapFrom(cliente => cliente.Endereco));
            CreateMap<UpdateClienteDto, Cliente>();
        }
    }
}