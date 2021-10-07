using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _repository;

        public UsuariosController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index() 
        {
            var listaUsuario = await _repository.ObtenerListaUsuarios();
            return View(listaUsuario);
        }

        public async Task<IActionResult> NuevoUsuario() 
        {
            var model = await _repository.NuevoUsuarioCreacion();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(UsuarioCreacionModel model)
        {
            if (ModelState.IsValid) 
            {
                //Guardar el usuario en la BD
                await _repository.InsertarUsuario(model);
               
                return RedirectToAction("Index","Usuarios");
            }
            return View("NuevoUsuario", model);
        }
        public async Task<IActionResult> ActualizarUsuario([FromRoute] int Id) 
        {
            var usuario = await _repository.ObtenerUsuarioPorID(Id);

            return View(usuario);
        }

        public IActionResult CambiarPassword( int Id) 
        {
            ViewBag.IdUsuario = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(UsuarioCambioPasswordModel model)
        {
            await _repository.ActualizarPassword(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _repository.ObtenerUsuarioPorID(id);

            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(UsuarioEdicionModel model)
        {
            await _repository.EliminarUsuario(model.Id);

            return RedirectToAction("Index");
        }
    }
}
