using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertarRol(RolCreacionModel model)
        {
            var nuevoRol = new Rol()
            {
                Nombre = model.Nombre
            };
            _context.Roles.Add(nuevoRol);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RolListaModel>> ObtenerListaRoles()
        {
            var respuesta = new List<RolListaModel>();
            var listaBD = await _context.Roles.ToListAsync();

            foreach (var rolDB in listaBD)
            {
                //Mapeo de entidades
                var newRolLista = new RolListaModel()
                {
                    Id = rolDB.Id,
                    Nombre = rolDB.Nombre,
                };
                respuesta.Add(newRolLista);
            }

            return respuesta;
        }
        public async Task<RolListaModel> ObtenerRolPorID(int Id)
        {
            var respuesta = new RolListaModel()
            {

            };
            var roldb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == Id); //Linq
            if (roldb != null)
            {
                respuesta.Id = roldb.Id;
                respuesta.Nombre = roldb.Nombre;
            }
            return respuesta;
        }


        public async Task ActualizarRol(RolListaModel model)
        {
            var roldb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);
            roldb.Nombre = model.Nombre;
            await _context.SaveChangesAsync();

        }


    }
}
