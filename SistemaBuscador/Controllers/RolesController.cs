using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolRepository _repository;

        public RolesController(IRolRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var listaRoles = await _repository.ObtenerListaRoles();
            return View(listaRoles);
        }
        public IActionResult NuevoRol()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoRol(RolCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                //Guardar el usuario en la BD
                await _repository.InsertarRol(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> ActualizarRol(int Id)
        {
            var rol = await _repository.ObtenerRolPorID(Id);

            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarRol(RolEdicionModel model)
        {
            if (ModelState.IsValid)
            {
                //Actualizar el usuario en la BD
                await _repository.ActualizarRol(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> EliminarRol(int id)
        {
            var rol = await _repository.ObtenerRolPorID(id);

            return View(rol);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarRol(RolEdicionModel model)
        {
            await _repository.EliminarRol(model.Id);

            return RedirectToAction("Index");
        }
    }
}
