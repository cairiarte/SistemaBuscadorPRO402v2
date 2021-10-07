using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Controllers;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using SistemaBuscador.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasIntegracion
{
    public class UsuarioControllerTest :TestBase
    {
        [TestClass]
        public class RolControllerTest : TestBase
        {
            [TestMethod]
            public async Task ObtenerListaUsuarios()
            {
                //Preparacion
                var nombreDB = Guid.NewGuid().ToString();
                var context = BuildContext(nombreDB);
                var RolService = new RolRepository(context);
                var SeguridadService = new Seguridad();
                var UsuarioService = new UsuarioRepository(context, SeguridadService, RolService);

                context.Usuarios.Add(new Usuario() { NombreUsuario = "UserTest1", Nombres = "RolTest1", Apellidos = "ApellidoTest1", Password = "PsswordTest1", RolId = 1 });
                context.Usuarios.Add(new Usuario() { NombreUsuario = "UserTest2", Nombres = "RolTest2", Apellidos = "ApellidoTest2", Password = "PsswordTest2", RolId = 1 });
                await context.SaveChangesAsync();

                //Ejecucion
                var context2 = BuildContext(nombreDB);
                var lista = await context2.Usuarios.ToListAsync();
                var resultado = lista.Count();

                //Validacion
                Assert.AreEqual(2, resultado);

            }

            [TestMethod]
            public async Task NuevoUsuario()
            {
                //Preparacion
                var nombreDB = Guid.NewGuid().ToString();
                var context = BuildContext(nombreDB);
                var SeguridadService = new Seguridad();
                var RolService = new RolRepository(context);
                var repo = new UsuarioRepository(context, SeguridadService, RolService);
                var model = new UsuarioCreacionModel() { Username="UserTest", Nombres = "RolTest", Apellidos="ApellidoTest", Password = "PsswordTest", RolId=1  };
                var controller = new UsuariosController(repo);

                //Ejecucion
                await controller.NuevoUsuario(model);
                var context2 = BuildContext(nombreDB);
                var lista = await context2.Usuarios.ToListAsync();
                var resultado = lista.Count();
                //Validacion

                Assert.AreEqual(1, resultado);

            }

            [TestMethod]
            public async Task ActualizarUsuario()
            {
                //Preparacion
                var nombreDB = Guid.NewGuid().ToString();
                var context = BuildContext(nombreDB);
                var SeguridadService = new Seguridad();
                var RolService = new RolRepository(context);
                var repo = new UsuarioRepository(context, SeguridadService, RolService);
                var usuario = new Usuario() {  NombreUsuario = "UserTest", Nombres = "RolTest", Apellidos = "ApellidoTest", Password = "PsswordTest", RolId = 1 };
                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();

                var model = new UsuarioEdicionModel() { Id = 1, Username = "UserTest", Nombres = "Usuario modificado", Apellidos = "ApellidoTest", RolId = 1 };
                var controller = new UsuariosController(repo);

                //Ejecucion
                await controller.ActualizarUsuario(model);
                var context2 = BuildContext(nombreDB);
                var UsuarioDB = await context2.Usuarios.FirstOrDefaultAsync(x => x.id == 1);
                var resultado = UsuarioDB.Nombres;
                //Validacion

                Assert.AreEqual("Usuario modificado", resultado);

            }

        }
    }
}
