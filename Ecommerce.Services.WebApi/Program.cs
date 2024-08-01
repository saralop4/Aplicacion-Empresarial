using Ecommerce.Services.WebApi.Helpers;
using Ecommerce.Services.WebApi.Modules.Authentication;
using Ecommerce.Services.WebApi.Modules.Feature;
using Ecommerce.Services.WebApi.Modules.HealthChecks;
using Ecommerce.Services.WebApi.Modules.Injection;
using Ecommerce.Services.WebApi.Modules.Mapper;
using Ecommerce.Services.WebApi.Modules.Swagger;
using Ecommerce.Services.WebApi.Modules.Validator;
using Ecommerce.Services.WebApi.Modules.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text;



namespace Ecommerce.Services.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           // var Configuration = builder.Configuration;
            ConfigureServices(builder.Services, builder.Configuration);

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {

                    app.UseSwagger(); //habilitamos el middleware para servir al swagger generated como un endpoint json
                    app.UseSwaggerUI( // habilitamos el dashboard de swagger 
                        c =>
                        {
                            //SwaggerEndpoint ese metodo recibe dos parametros, el primero es la url, el segundo es el nombre del endpoint
                            //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Api Empresarial v1");

                            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                            foreach (var description in provider.ApiVersionDescriptions)
                            {
                                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                            }
                        });
                }

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecksUI();

            //con este endpoint el cliente puede consumir en tiempo real el estado de salud del microservicio ya que se actualiza cada 5 segundo en tiempo real
            app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

                //como primer parametro colocamos el path del endpoint y como segundo parametro especificamos la estructura de respuesta
            });
                app.Run();
            
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddVersioning();
            services.AddAuthentication(configuration);
            services.AddMapper();
            services.AddFeature(configuration);
            services.AddValidator();
            services.AddHealthCheck(configuration);



            //    /*************************ESTA ES UNA FORMA LARGA DE CONFIGURAR EL SWAGGER CON AUTENTICACION JWT**************************/
            //builder.Services.AddSwaggerGen(c =>
            //{

            //    var XmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, XmlFile);
            //    c.IncludeXmlComments(xmlPath);

            //    //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    //    {
            //    //        Description = "Ingrese el token JWT así: Bearer {su token}",
            //    //        In = ParameterLocation.Header,//ubicacion donde se va a enviar el token bearer es decir en el header
            //    //       // Type = SecuritySchemeType.ApiKey,
            //    //       Type= SecuritySchemeType.Http, //este esquema nos permite enviar un token de autorizacion en el header
            //    //        Name = "Authorization",
            //    //        Scheme = "bearer",
            //    //        BearerFormat= "JWT"
            //    //    });

            //    //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    //        {
            //    //            {
            //    //                new OpenApiSecurityScheme
            //    //                {
            //    //                    Reference = new OpenApiReference
            //    //                    {
            //    //                        Type = ReferenceType.SecurityScheme,
            //    //                        Id = JwtBearerDefaults.AuthenticationScheme
            //    //                    }
            //    //                },
            //    //                new List<string>()
            //    //            }
            //    //    });

            //    //});

            //    /******************FINAL - ESTA ES UNA FORMA LARGA DE CONFIGURAR EL SWAGGER CON AUTENTICACION JWT*******************/



            //    /******************FORMA CORTA*******************/
            //    var securityScheme = new OpenApiSecurityScheme
            //        {
            //            Description = "Ingrese el token JWT **_only_**",
            //            In = ParameterLocation.Header,//ubicacion donde se va a enviar el token bearer es decir en el header
            //            Type = SecuritySchemeType.Http, //este esquema nos permite enviar un token de autorizacion en el header
            //            Name = "Authorization",
            //            Scheme = "bearer",
            //            BearerFormat = "JWT",
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = JwtBearerDefaults.AuthenticationScheme
            //            }

            //        };

            //        c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            //        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //        {
            //            {securityScheme,  new List<string>() { } }
            //        });
            //    /******************FINAL - FORMA CORTA*******************/
            //});


            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); //de esta forma indicamos que en el proceso
                                                                                                                           //de serializar y deserializar objetos json va a tomar la resolucion predeterminada, pero se puede usar otro tipo de resolucion, segun la necesidad,
                                                                                                                           //por ejemplo camelcase.  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


            });


            //en esta parte ya se configura como tal la comunicacion o mapeo entre el archivo json appsettings y la clase AppSettings
            var appSettingsSection = configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);

            // Configura la autenticación JWT
            var appSettings = appSettingsSection.Get<AppSettings>();// creamos una instancia de la clase AppSettings para tener acceso a las propiedades de esa clase
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;


            // Inyección de dependencias
            services.AddInjection(configuration);

            services.AddSwaggerDocumentation();
        }

    }
}
