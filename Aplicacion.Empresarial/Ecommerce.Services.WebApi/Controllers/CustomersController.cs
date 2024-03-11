using Ecommerce.Aplicacion.DTO;
using Ecommerce.Aplicacion.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerAplicacion _customerAplicacion;

        public CustomersController(ICustomerAplicacion customerAplicacion)
        {
            _customerAplicacion = customerAplicacion;
        }

        #region Metodos Sincronos*

        [HttpPost("InsertSinc")]
        public IActionResult Insert([FromBody] CustomersDto customersDto)
        {
            if (customersDto == null)
            {
                return BadRequest();
            }
            var response = _customerAplicacion.Insert(customersDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpPut("UpdateSinc")]
        public IActionResult Update([FromBody] CustomersDto customersDto)
        {
            if (customersDto == null)
            {
                return BadRequest();
            }
            var response = _customerAplicacion.Update(customersDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpDelete("DeleteSinc")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response = _customerAplicacion.Delete(customerId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpGet("GetSinc")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response = _customerAplicacion.Get(customerId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpGet("GetAllSinc")]
        public IActionResult GetAll()
        {

            var response = _customerAplicacion.GetAll();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        #endregion


        #region Metodos Asincronos*

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomersDto customersDto)
        {
            if (customersDto == null)
            {
                return BadRequest();
            }
            var response = await _customerAplicacion.InsertAsync(customersDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomersDto customersDto)
        {
            if (customersDto == null)
            {
                return BadRequest();
            }
            var response = await _customerAplicacion.UpdateAsync(customersDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpDelete("{customerId}")]
        public async  Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response =await  _customerAplicacion.DeleteAsync(customerId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpGet("GetAsync")]
        public async  Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }
            var response = await _customerAplicacion.GetAsync(customerId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {

            var response = await _customerAplicacion.GetAllAsync();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);


        }

        #endregion

    }
}
