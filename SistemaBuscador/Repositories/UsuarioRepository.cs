using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeguridad _seguridad;
        private readonly IRolRepository _rolRepository;

        public UsuarioRepository(ApplicationDbContext context, ISeguridad seguridad, IRolRepository rolRepository)
        {
            _context = context;
            _seguridad = seguridad;
            _rolRepository = rolRepository;
        }
        public async Task InsertarUsuario(UsuarioCreacionModel model)
        {
            var nuevoUsuario = new Usuario()
            {
                NombreUsuario = model.Username,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                RolId = (int)model.RolId,
                Password = _seguridad.Encriptar(model.Password)
            };
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UsuarioListaModel>> ObtenerListaUsuarios()
        {
            var respuesta = new List<UsuarioListaModel>();
            var listaBD = await _context.Usuarios.ToListAsync();

            foreach (var usuarioDB in listaBD)
            {
                //Mapeo de entidades
                var newUsuarioLista = new UsuarioListaModel(){
                    Id = usuarioDB.id,
                    Username = usuarioDB.NombreUsuario,
                    Nombres = usuarioDB.Nombres,
                    Apellidos = usuarioDB.Apellidos,
                    Rol = usuarioDB.RolId
                };
                respuesta.Add(newUsuarioLista);
            }

            return respuesta;
        }
        public async Task<UsuarioEdicionModel> ObtenerUsuarioPorID(int Id) 
        {
            var respuesta = new UsuarioEdicionModel() {

            };
            var usuariodb = await _context.Usuarios.FirstOrDefaultAsync(x => x.id == Id); //Linq
            if(usuariodb != null) 
            {
                respuesta.Id = usuariodb.id;
                respuesta.Username = usuariodb.NombreUsuario;
                respuesta.Nombres = usuariodb.Nombres;
                respuesta.Apellidos = usuariodb.Apellidos;
                respuesta.RolId = usuariodb.RolId;
            }
            return respuesta;
        }


        public async Task ActualizarUsuario(UsuarioEdicionModel model) 
        {
            var usuariodb = await _context.Usuarios.FirstOrDefaultAsync(x => x.id == model.Id);
            usuariodb.NombreUsuario = model.Username;
            usuariodb.Nombres = model.Nombres;
            usuariodb.Apellidos = model.Apellidos;
            usuariodb.RolId = (int)model.RolId;
            await _context.SaveChangesAsync();

        }
        
        public async Task ActualizarPassword(UsuarioCambioPasswordModel model)
        {
            var usuariodb = await _context.Usuarios.FirstOrDefaultAsync(x => x.id == model.Id);
            usuariodb.Password = _seguridad.Encriptar(model.Password);

            await _context.SaveChangesAsync();

        }

        public async Task EliminarUsuario(int Id) 
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.id == Id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<UsuarioCreacionModel> NuevoUsuarioCreacion() 
        {
            var roles = await _rolRepository.ObtenerListaRoles();
            var respuesta = new UsuarioCreacionModel();
            respuesta.Roles = new SelectList(roles, "Id", "Nombre");

            return respuesta;
        }
    }
}
