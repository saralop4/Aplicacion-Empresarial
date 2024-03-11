using AutoMapper;
using Ecommerce.Aplicacion.DTO;
using Ecommerce.Dominio.Entity;
using System;

namespace Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {

            //de  esta forma se hace un mapeo de todos los campos porque tienen los mismos
            CreateMap<Customers, CustomersDto>().ReverseMap();

            //de esta forma se hace un mapeo por individual cuando las dos clases no tienen los mismos atributos,
            //DESTINO Y ORIGEN
         /*   CreateMap<Customers, CustomersDto>().ReverseMap()
                .ForMember(destination => destination.CustomerId, source => source.MapFrom(src =>src.CustomerId))
                .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName))
                .ForMember(destination => destination.ContactName, source => source.MapFrom(src => src.ContactName))
                .ForMember(destination => destination.ContactTitle, source => source.MapFrom(src => src.ContactTitle))
                .ForMember(destination => destination.Address, source => source.MapFrom(src => src.Address)).ReverseMap();
       */
            
        }
    }
}
