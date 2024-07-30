using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Ecommerce.Application.Test
{
    [TestClass]
    public class UsersApplicationTest
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory; //nos permite crear diferentes servicios dentro de un alcance local

        [ClassInitialize]
        public static void Initialize(TestContext context) //este metodo se ejecuta primero que los metodos de pruebas.
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
                _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services= new ServiceCollection();
            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();     

        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            //Arrange: Donde se inicializa los objetos necesarios para la ejecucion del codigo
            var userName = string.Empty;
            var password = string.Empty;
            var expected = "Errores de Validacion";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.
            Assert.AreEqual(expected, actual); //este metodo nos permite comprobar si dos valores son iguales

        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametrosCorrectos_RetornarMensajeExitoso()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            //Arrange: Donde se inicializa los objetos necesarios para la ejecucion del codigo
            var userName = "admin";
            var password = "12345";
            var expected = "Autenticacion Exitosa!!!";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.
            Assert.AreEqual(expected, actual); //este metodo nos permite comprobar si dos valores son iguales

        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<IUsersApplication>();

            //Arrange: Donde se inicializa los objetos necesarios para la ejecucion del codigo
            var userName = "admin";
            var password = "12345789";
            var expected = "Usuario no existe";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.
            Assert.AreEqual(expected, actual); //este metodo nos permite comprobar si dos valores son iguales

        }
    }
}
