using Ecommerce.Aplicacion.DTO;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Common;
using Ecommerce.Services.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")] 
    public class UsersController : ControllerBase
    {
        private readonly IUsersAplicacion _usersAplicacion;
        private readonly AppSettings _appSettings;

        public UsersController(IUsersAplicacion usersAplicacion, IOptions<AppSettings> appSettings)
        {
            _usersAplicacion = usersAplicacion;
            _appSettings = appSettings.Value;
        }


        //el unico metodo que debe permitir autenticaciones anonimas es este ya que este metodo es quien genera el token
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UsersDto usersDto)
        {
            //response va almacenar la respuesta del metodo Authenticate
            var response = _usersAplicacion.Authenticate(usersDto.UserName, usersDto.Password);

            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }

            }

            return BadRequest(response.Message);
        }

        private string BuildToken(Response<UsersDto> usersDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();//esta clase es diseñada para crear y validar los jwt

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret); //obtenemos la key de la clase appsettings y lo convertimos a un array de bytes
            var tokenDescriptor = new SecurityTokenDescriptor //esta clase permite especificar los atributos para emision del token
            {
                Subject = new ClaimsIdentity(new Claim[] //el atributo Subject nos permite especificar la informacion que contendrá el token
                {
                    new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString()) //hay diferentes tipos de ClaimTypes
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), //atributo de cuando va a expirar el token, en este caso durara 1 minuto
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), //este atributo nos permite especificar las credenciales que se utilizaran para firmar el token
                //en este caso será la clave privada que esta en el archivo appsettings.json o clase AppSettings
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
