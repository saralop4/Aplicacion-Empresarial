using Microsoft.AspNetCore.Mvc.Versioning;

namespace Ecommerce.Services.WebApi.Modules.Versioning
{
    public static  class VersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
                //  o.ApiVersionReader = new HeaderApiVersionReader("x-version"); //aqui enviamos el parametro en el encabezado de la solicitud
                // o.ApiVersionReader = new QueryStringApiVersionReader("api-version");//aqui enviamos el parametro como query parametro
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true; //esta propiedad nos sirve para indicar que se va a reemplazar un segmento de url por la version de la api

            });
            return services;    
                
        }

    }
}
