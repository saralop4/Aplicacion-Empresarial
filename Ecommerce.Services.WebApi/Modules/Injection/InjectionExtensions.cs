﻿using Ecommerce.Aplicacion.Interface;
using Ecommerce.Aplicacion.Main;
using Ecommerce.Aplicacion.Validator;
using Ecommerce.Transversal.Common.Interfaces;
using Ecommerce.Dominio.Core;
using Ecommerce.Dominio.Interfaces;
using Ecommerce.Infraestructura.Data;
using Ecommerce.Infraestructura.Interfaces;
using Ecommerce.Infraestructura.Repository;
using Ecommerce.Transversal.Logging;

namespace Ecommerce.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {

        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<DapperContext>(); //se necesita que una sola vez se conecte a la baase de datos y
            //y esa misma instancia de conexion se reutilice
            services.AddScoped<ICustomerAplicacion, CustomersAplicacion>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersAplicacion, UsersAplicacion>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<UsersDtoValidator>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();   

            return services;    
        }


    }
}
