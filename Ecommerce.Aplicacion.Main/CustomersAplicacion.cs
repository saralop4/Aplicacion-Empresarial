using AutoMapper;
using Ecommerce.Aplicacion.DTO;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Transversal.Common;
using Ecommerce.Transversal.Common.Interfaces;
using Ecommerce.Dominio.Entity;
using Ecommerce.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Aplicacion.Main
{
    public class CustomersAplicacion :ICustomerAplicacion
    {
        private readonly ICustomersDomain _customersDomain;
        private readonly IMapper _mapper; //esta es una interfaz propia de AutoMapper
        private readonly IAppLogger<CustomersAplicacion> _logger;
        public CustomersAplicacion(ICustomersDomain customersDomain, IMapper mapper, IAppLogger<CustomersAplicacion> logger)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;   
            _logger = logger;   
        }

        #region Metodo Sincronos
        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();

            try
            {                
                response.Data = _customersDomain.Delete(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Elimiacion Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public Response<CustomersDto> Get(string customerId)
        {
            var response = new Response<CustomersDto>();

            try
            {
                var customers = _customersDomain.Get(customerId);
                response.Data = _mapper.Map<CustomersDto>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public Response<IEnumerable<CustomersDto>> GetAll()
        {
            var response = new Response<IEnumerable<CustomersDto>>();

            try
            {
                var customers = _customersDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!";
                    _logger.LogInformation("Consulta Exitosa!!");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message);

            }
            return response;
        }
        public Response<bool> Insert(CustomersDto customerDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Insert(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public Response<bool> Update(CustomersDto customerDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Insert(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public ResponsePagination<IEnumerable<CustomersDto>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomersDto>>();

            try
            {
                var count = _customersDomain.Count();
                var customers = _customersDomain.GetAllWithPagination(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);

                if(response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta Paginada Exitosa!!!";

                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion

        #region Metodos Asyncronos

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = await _customersDomain.DeleteAsync(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Elimiacion Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }
        public async Task<Response<IEnumerable<CustomersDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDto>>();

            try
            {
                var customers = await _customersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public async Task<Response<CustomersDto>> GetAsync(string customerId)
        {
            var response = new Response<CustomersDto>();

            try
            {
                var customers = await _customersDomain.GetAsync(customerId);
                response.Data = _mapper.Map<CustomersDto>(customers); //se mapea el resultado que vaya a tener la variable customers con el dto
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

       
        public async Task<Response<bool>> InsertAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.InsertAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }

        public  async Task<Response<bool>> UpdateAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.UpdateAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualizacion Exitosa!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }
            return response;
        }
        public async Task<ResponsePagination<IEnumerable<CustomersDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomersDto>>();

            try
            {
                var count = await _customersDomain.CountAsync();
                var customers = await _customersDomain.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta Paginada Exitosa!!!";

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion 
    }
}
