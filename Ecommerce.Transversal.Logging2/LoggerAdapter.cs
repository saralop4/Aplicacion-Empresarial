using Ecommerce.Common.Interfaces;
using Microsoft.Extensions.Logging;  //componente nugget

namespace Ecommerce.Transversal.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T> 
    {
        private readonly ILogger<T> _logger;

        /*En la consola de visual studio se muestan los logs que hemos generado con cada uno de los metodos,
         estos metodos son utilizados en los controladores o tambien pueden ser uilizados en la logica
        ya que son para mostrar en consola mensajes de exitos o errores */

        /*Estos logs se pueden persistir en archivos de textos, base de datos, archivos xml o en lo que
         uno desee o crea conveniente*/


        public LoggerAdapter(ILoggerFactory loggerFactory) //esta interfaz nos permite crear los sistemas de loggin y trazabilidad de las excepciones
        {
            _logger= loggerFactory.CreateLogger<T>();   
        }
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);  //estos metodos son propios del componente nugget que se instaló

        }
      public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);  

        }
      public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);

        }

    }
}
