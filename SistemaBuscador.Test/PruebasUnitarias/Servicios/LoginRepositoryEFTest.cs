using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaBuscador.Repositories;
using SistemaBuscador.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class LoginRepositoryEFTest : TestBase
    {
        [TestMethod]
        public async Task UsuarioNoExiste() 
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBd);
            context.Usuarios.Add(new Entities.Usuario() { NombreUsuario = "Usuario1", Password = "Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreBd);
            var seguridad = new Mock<ISeguridad>();
            seguridad.Setup(x => x.Encriptar(It.IsAny<string>())).Returns("aabbccddeeffgghhii");

            //Ejecucion
            var nombreUsuario = "Usuario2";
            var Password = "Password2";
            var repo = new LoginRepositoryEF(context2,seguridad.Object);
            var respuesta = await repo.UserExist(nombreUsuario, Password);

            //Verificacion
            Assert.IsFalse(respuesta);
        }
        [TestMethod]
        public async Task UsuarioExiste() 
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBd);
            context.Usuarios.Add(new Entities.Usuario() { NombreUsuario = "Usuario1", Password = "aabbccddeeffgghhii" });
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreBd);
            var seguridad = new Mock<ISeguridad>();
            seguridad.Setup(x => x.Encriptar(It.IsAny<string>())).Returns("aabbccddeeffgghhii");

            //Ejecucion
            var nombreUsuario = "Usuario1";
            var Password = "Password1";
            var repo = new LoginRepositoryEF(context2,seguridad.Object);
            var respuesta = await repo.UserExist(nombreUsuario, Password);

            //Verificacion
            Assert.IsTrue(respuesta);

        }
    }

}
