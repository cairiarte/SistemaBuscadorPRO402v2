using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Controllers;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasIntegracion
{
    [TestClass]
    public class RolControllerTest : TestBase
    {

        [TestMethod]
        public async Task ObtenerListaRoles() 
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var RolService = new RolRepository(context);

            context.Roles.Add(new Rol() { Nombre = "Administrador" });
            context.Roles.Add(new Rol() { Nombre = "Supervisor" });
            await context.SaveChangesAsync();

            //Ejecucion
            var context2 = BuildContext(nombreDB);
            var lista = await context2.Roles.ToListAsync();
            var resultado = lista.Count();

            //Validacion
            Assert.AreEqual(2, resultado);

        }

        [TestMethod]
        public async Task NuevoRol() 
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var RolService = new RolRepository(context);
            var model = new RolCreacionModel() { Nombre = "RolTest" };
            var controller = new RolesController(RolService);

            //Ejecucion
            await controller.NuevoRol(model);
            var context2 = BuildContext(nombreDB);
            var lista = await context2.Roles.ToListAsync();
            var resultado = lista.Count();
            //Validacion

            Assert.AreEqual(1, resultado);

        }

        [TestMethod]
        public async Task ActualizarRol()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var RolService = new RolRepository(context);
            var rol = new Rol() { Nombre = "RolTest" };
            context.Roles.Add(rol);
            await context.SaveChangesAsync();
            var model = new RolEdicionModel() { Id=1, Nombre = "RolTest Modificado" };
            var controller = new RolesController(RolService);

            //Ejecucion
            await controller.ActualizarRol(model);
            var context2 = BuildContext(nombreDB);
            var RolDB = await context2.Roles.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado = RolDB.Nombre;
            //Validacion

            Assert.AreEqual("RolTest Modificado", resultado);
        }
    }
}
