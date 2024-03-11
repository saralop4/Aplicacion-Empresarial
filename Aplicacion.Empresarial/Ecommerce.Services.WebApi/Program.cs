using AutoMapper;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Aplicacion.Main;
using Ecommerce.Domain.Core;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Data;
using Ecommerce.Infraestructura.Interfaces;
using Ecommerce.Infraestructura.Repository;
using Ecommerce.Transversal.Interfaces;
using Ecommerce.Transversal.Mapper;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservicio",
                    Description = "Microservicio Empresarial Web Api",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sarah Lopez",
                        Email = "saralopezjb16@gmail.com",
                        Url = new Uri("https://Ecommerce.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Sisma Corporation",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var XmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, XmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));

            builder.Services.AddCors(options => options.AddPolicy(MyPolicy, builder => builder.WithOrigins(Configuration["Config:OriginsCors"])
                                                                                              .AllowAnyHeader()
                                                                                              .AllowAnyMethod()));

            builder.Services.AddControllers()
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
              });

            builder.Services.AddSingleton<IConfiguration>(Configuration);
            builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(); //se necesita que una sola vez se conecte a la baase de datos y
            //y esa misma instancia de conexion se reutilice
            builder.Services.AddScoped<ICustomerAplicacion, CustomersAplicacion>();
            builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();


            var app = builder.Build();

           if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        //SwaggerEndpoint ese metodo recibe dos parametros, el primero es la url, el segundo es el nombre del endpoint
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Api Empresarial v1");
                    });
            }

       
            app.UseAuthorization();
            app.UseCors(MyPolicy);
           // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.MapControllers();
            app.Run();
        }
    }
}
