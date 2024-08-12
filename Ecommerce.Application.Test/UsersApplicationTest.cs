using Ecommerce.Services.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ecommerce.Aplicacion.Interface;
using Ecommerce.Application.Test;

namespace Pacagroup.Ecommerce.Application.Test
{
    [TestClass]
    public class UsersApplicationTest
    {
        private static WebApplicationFactory<Program> _factory = null;
        private static IServiceScopeFactory _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext _) //este metodo se ejecuta primero que los metodos de pruebas.
        {
            _factory = new CustomWebApplicationFactory(); //el codigo que iria aqui se resumió en la clase CustomWebApplicationFactory
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>(); //la interfaz sirve para crear nuevos servicios
        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersAplicacion>();

            //Arrange: Donde se inicializa los objetos necesarios para la ejecucion del codigo
            var userName = string.Empty;
            var password = string.Empty;
            var expected = "Errores de Validacion";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.      
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersAplicacion>();

            // Arrange
            var userName = "admin";
            var password = "12345";
            var expected = "Autenticacion Exitosa";

            // Act
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersAplicacion>();

            // Arrange
            var userName = "admin";
            var password = "123456899";
            var expected = "Usuario no existe";

            // Act
            var result = context.Authenticate(userName, password);
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
