using Ecommerce.Services.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Services.WebApi.Modules.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //en esta parte ya se configura como tal la comunicacion o mapeo entre el archivo json appsettings y la clase AppSettings
            var appSetingsSection = configuration.GetSection("Config");

            services.Configure<AppSettings>(appSetingsSection);

            // Configura la autenticación JWT
            var appSettings = appSetingsSection.Get<AppSettings>();// creamos una instancia de la clase AppSettings para tener acceso a las propiedades de esa clase

            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var Issuer = appSettings.Issuer;
            var Audience = appSettings.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //con estos atributos decimos que el token va a ser 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  //del tipo JwrBearer
            })

            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false, // Asegúrate de que esta configuración es la deseada
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name); //el metodo ontokevalidated valida obteniendo la informacion desde los claims
                        return Task.CompletedTask; //que hemos generado y de ahi mismo se puede obtener todos los valores para el token como se desee personalizar
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
            });

            return services;

        }
    }
}
