using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using SistemaBuscador.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class UsuarioRepositoryTest : TestBase
    {


        [TestMethod]
        public async Task InsertarUsuario()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var ServiceSeguridad = new Mock<ISeguridad>();
            ServiceSeguridad.Setup(x => x.Encriptar(It.IsAny<String>())).Returns("AABBCCDDEEFFGGHH");

            var ServiceRol = new Mock<IRolRepository>();
            var repo = new UsuarioRepository(context,ServiceSeguridad.Object,ServiceRol.Object);
            var model = new UsuarioCreacionModel() { Username = "UsuarioTest", Nombres = "NombreTest", Apellidos = "ApellidoTest", Password = "PasswordTest", RolId = 1 };

            //Ejecucion
            await repo.InsertarUsuario(model);
            var context2 = BuildContext(nombreDB);
            var lista = await context2.Usuarios.ToListAsync();
            var resultado = lista.Count();

            //Validacion
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public async Task ObtenerUsuarioPorId()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var ServiceSeguridad = new Mock<ISeguridad>();
            var ServiceRol = new Mock<IRolRepository>();
            var usuario = new Usuario() { NombreUsuario = "UsuarioTest", Nombres = "NombreTest", Apellidos = "ApellidoTest", Password = ServiceSeguridad.Object.Encriptar("PasswordTest"), RolId = (int)1 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            var context2 = BuildContext(nombreDB);
            var repo = new UsuarioRepository(context2,ServiceSeguridad.Object, ServiceRol.Object);

            //Ejecucion
            var usuarioDB = await repo.ObtenerUsuarioPorID(1);

            //Validacion
            Assert.IsNotNull(usuarioDB);
        }

        [TestMethod]
        public async Task ActualizarUsuario()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var ServiceSeguridad = new Mock<ISeguridad>();
            var ServiceRol = new Mock<IRolRepository>();

            var usuario = new Usuario() { NombreUsuario = "UsuarioTest", Nombres = "NombreTest", Apellidos = "ApellidoTest", Password = ServiceSeguridad.Object.Encriptar("PasswordTest"), RolId = (int)1 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreDB);
            var repo = new UsuarioRepository(context2, ServiceSeguridad.Object, ServiceRol.Object);
            var model = new UsuarioEdicionModel() { Id=1, Username = "UsuarioTest2", Nombres = "NombreTest2", Apellidos = "ApellidoTest2", RolId = (int)1 };

            //Ejecucion
            await repo.ActualizarUsuario(model);
            var context3 = BuildContext(nombreDB);
            var usuariomodificado = await context3.Usuarios.FirstOrDefaultAsync(x => x.id == 1);
            var resultado = usuariomodificado.Nombres;
            //Validacion
            Assert.AreEqual("NombreTest2", resultado);
        }

        [TestMethod]
        public async Task EliminarUsuario()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var ServiceSeguridad = new Mock<ISeguridad>();
            var ServiceRol = new Mock<IRolRepository>();

            var usuario = new Usuario() { NombreUsuario = "UsuarioTest", Nombres = "NombreTest", Apellidos = "ApellidoTest", Password = ServiceSeguridad.Object.Encriptar("PasswordTest"), RolId = (int)1 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreDB);
            var repo = new UsuarioRepository(context2, ServiceSeguridad.Object, ServiceRol.Object);


            //Ejecucion
            await repo.EliminarUsuario(1);
            var context3 = BuildContext(nombreDB);
            var ListaUsuario = await context3.Usuarios.ToListAsync();
            var resultado = ListaUsuario.Count;
            //Validacion
            Assert.AreEqual(0, resultado);
        }
    }

}