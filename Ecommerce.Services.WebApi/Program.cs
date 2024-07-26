using AutoMapper;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Aplicacion.Main;
using Ecommerce.Aplicacion.Validator;
using Ecommerce.Common.Interfaces;
using Ecommerce.Domain.Core;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Data;
using Ecommerce.Infraestructura.Interfaces;
using Ecommerce.Infraestructura.Repository;
using Ecommerce.Services.WebApi.Helpers;
using Ecommerce.Services.WebApi.Modules.Swagger;
using Ecommerce.Services.WebApi.Modules.Validator;
using Ecommerce.Services.WebApi.Modules.Versioning;
using Ecommerce.Transversal.Interfaces;
using Ecommerce.Transversal.Logging;
using Ecommerce.Transversal.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;



namespace Ecommerce.Services.WebApi
{
    public class Program
    {

        public static string MyPolicy { get; } = "policyApiEcommerce";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


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

            //  builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingsProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            builder.Services.AddCors(options => options.AddPolicy(MyPolicy, builder => builder.WithOrigins(Configuration["Config:OriginsCors"]) //originis  permitidos con la key OriginsCors

                                                                                              .AllowAnyHeader() //Permite que el cliente envíe cualquier encabezado en la solicitud.
                                                                                                                //Esto es útil cuando no quieres restringir los encabezados que el
                                                                                                                //cliente puede enviar al servidor.
                                                                                              .AllowAnyMethod()  //que permita todos los metodos
                                                                                              .WithExposedHeaders("Content-Disposition"))); //Especifica qué encabezados pueden
                                                                                                                                            //ser leídos por el cliente en la respuesta.
                                                                                                                                            //En tu caso, estás exponiendo específicamente
                                                                                                                                            //el encabezado Content-Disposition.
                                                                                           // .WithHeaders() Si deseas restringir los encabezados que el cliente puede enviar, puedes usar.WithHeaders() en lugar de.AllowAnyHeader()


            builder.Services.AddControllers()
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); //de esta forma indicamos que en el proceso
                  //de serializar y deserializar objetos json va a tomar la resolucion predeterminada, pero se puede usar otro tipo de resolucion, segun la necesidad,
                  //por ejemplo camelcase.  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
              });

            //en esta parte ya se configura como tal la comunicacion o mapeo entre el archivo json appsettings y la clase AppSettings
            var appSetingsSection = Configuration.GetSection("Config");

            builder.Services.Configure<AppSettings>(appSetingsSection);

            // Configura la autenticación JWT
            var appSettings = appSetingsSection.Get<AppSettings>();// creamos una instancia de la clase AppSettings para tener acceso a las propiedades de esa clase

            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var Issuer = appSettings.Issuer;
            var Audience = appSettings.Audience;

            builder.Services.AddAuthentication(options =>
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



            builder.Services.AddSingleton<IConfiguration>(Configuration);
            builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(); //se necesita que una sola vez se conecte a la baase de datos y
            //y esa misma instancia de conexion se reutilice
            builder.Services.AddScoped<ICustomerAplicacion, CustomersAplicacion>();
            builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<IUsersAplicacion, UsersAplicacion>();
            builder.Services.AddScoped<IUsersDomain, UsersDomain>();    
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            builder.Services.AddTransient<UsersDtoValidator>();


            builder.Services.AddVersioning(); //metodo de la clase VersioningExtension
            // builder.Services.AddAuthentication(this.configuration);
            // builder.Services.AddMapper();
            // builder.Services.AddFeature(this.configuration);
            // builder.Services.AddFeature(this.configuration);
            // builder.Services.AddFeature(this.configuration);
            builder.Services.AddValidator();
            



            builder.Services.AddSwaggerDocumentation();
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
            app.UseCors(MyPolicy); //agregamos en el pipeline el uso de cors, recibe el nombre de la politica
               
           // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.MapControllers();
            app.Run();
        }
    }
}
