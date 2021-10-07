using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class RolRepositorioTest :TestBase
    {
        [TestMethod]
        public async Task InsertarRol() 
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);
            var repo = new RolRepository(context);
            var model = new RolCreacionModel() { Nombre= "Rol Test" };

            //Ejecucion
            await repo.InsertarRol(model);
            var context2 = BuildContext(nombreDB);
            var lista = await context2.Roles.ToListAsync();
            var resultado = lista.Count();
            //Validacion
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public async Task ObtenerRolPorId() 
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);

            var rol = new Rol() { Nombre = "Rol1" };
            context.Roles.Add(rol);
            await context.SaveChangesAsync();
            var context2 = BuildContext(nombreDB);
            var repo = new RolRepository(context2);
            //Ejecucion
            var rolBD = await repo.ObtenerRolPorID(1);

            //Validacion
            Assert.IsNotNull(rolBD);
        }

        [TestMethod]
        public async Task ActualizarRol()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);

            var rol = new Rol() { Nombre = "Rol1" };
            context.Roles.Add(rol);
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreDB);
            var repo = new RolRepository(context2);
            var model = new RolEdicionModel() { Id = 1, Nombre="Rol1 Modificado" };

            //Ejecucion
            await repo.ActualizarRol(model);
            var context3 = BuildContext(nombreDB);
            var rolmodificado = await context3.Roles.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado = rolmodificado.Nombre;
            //Validacion
            Assert.AreEqual("Rol1 Modificado", resultado);
        }

        [TestMethod]
        public async Task EliminarRol()
        {
            //Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var context = BuildContext(nombreDB);

            var rol = new Rol() { Nombre = "Rol1" };
            context.Roles.Add(rol);
            await context.SaveChangesAsync();

            var context2 = BuildContext(nombreDB);
            var repo = new RolRepository(context2);


            //Ejecucion
            await repo.EliminarRol(1);
            var context3 = BuildContext(nombreDB);
            var ListaRoles = await context3.Roles.ToListAsync();
            var resultado = ListaRoles.Count;
            //Validacion
            Assert.AreEqual(0, resultado);
        }
    }
}
