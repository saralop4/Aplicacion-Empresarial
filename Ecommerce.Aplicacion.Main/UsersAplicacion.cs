using AutoMapper;
using Ecommerce.Aplicacion.DTO;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Aplicacion.Validator;
using Ecommerce.Common;
using Ecommerce.Dominio.Interfaces;
using System;

namespace Ecommerce.Aplicacion.Main
{
    public class UsersAplicacion : IUsersAplicacion
    {

        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper; //esta es una interfaz propia de AutoMapper
        private readonly UsersDtoValidator _usersDtovalidator;
        public UsersAplicacion(IUsersDomain usersDomain, IMapper mapper, UsersDtoValidator usersDtovalidator)
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
            _usersDtovalidator = usersDtovalidator; 
        }
        public Response<UsersDto> Authenticate(string username, string password)
        {
            var response = new Response<UsersDto>();   
            var validation = _usersDtovalidator.Validate(new UsersDto() { UserName = username, Password = password }); //esta variable almacena la respuesta del validador fluent

            if(!validation.IsValid)
            {
                response.Message = "Errores de Validacion";
                response.Errors =validation.Errors;
                return response;
            }

            try
            {
                var user = _usersDomain.Authenticate(username, password);   
                response.Data = _mapper.Map<UsersDto>(user); //se mapea la entidad de negocio hacia el dto
                response.IsSuccess = true;
                response.Message = "Autenticacion Exitosa";

            }
            catch(InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString(); 

            }
            return response;

        }
    }
}
