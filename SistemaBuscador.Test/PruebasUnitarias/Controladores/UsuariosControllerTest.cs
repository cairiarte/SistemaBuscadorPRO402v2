using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaBuscador.Controllers;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Controladores
{
    [TestClass]
    public class UsuariosControllerTest
    {
        [TestMethod]
        public async Task NuevoUsuario_Modelo_Invalido() 
        {
            //Preparacion
            var rolService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCreacionModel();
            var controller = new UsuariosController(rolService.Object);
            //Ejecucion
            controller.ModelState.AddModelError(String.Empty, "Datos invalidos");
            var resultado = await controller.NuevoUsuario(model) as ViewResult;
            //Validacion
            Assert.AreEqual(resultado.ViewName, "NuevoUsuario");
        }
        [TestMethod]
        public async Task NuevoUsuario_Modelo_Valido()
        {
            //Preparacion
            var rolService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCreacionModel();
            var controller = new UsuariosController(rolService.Object);
            //Ejecucion
            var resultado = await controller.NuevoUsuario(model) as RedirectToActionResult;
            //Validacion
            Assert.AreEqual(resultado.ControllerName, "Usuarios");
            Assert.AreEqual(resultado.ActionName, "Index");
        }
    }
}
