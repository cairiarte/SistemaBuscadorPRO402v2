using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioListaModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Rol { get; set; }
    }
}
