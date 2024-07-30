using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Ecommerce.Services.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {

        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {

        
            string MyPolicy = "policyApiEcommerce";


            services.AddCors(options => options.AddPolicy(MyPolicy, builder => builder.WithOrigins(configuration["Config:OriginsCors"]) //originis  permitidos con la key OriginsCors

                                                                                              .AllowAnyHeader() //Permite que el cliente envíe cualquier encabezado en la solicitud.
                                                                                                                //Esto es útil cuando no quieres restringir los encabezados que el
                                                                                                                //cliente puede enviar al servidor.
                                                                                              .AllowAnyMethod()  //que permita todos los metodos
                                                                                              .WithExposedHeaders("Content-Disposition"))); //Especifica qué encabezados pueden
                                                                                                                                            //ser leídos por el cliente en la respuesta.
                                                                                                                                            //En tu caso, estás exponiendo específicamente
                                                                                                                                            //el encabezado Content-Disposition.
                                                                                                                                            // .WithHeaders() Si deseas restringir los encabezados que el cliente puede enviar, puedes usar.WithHeaders() en lugar de.AllowAnyHeader()


            services.AddMvc();
            return services;

        }
    }
}
