using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioCreacionModel
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [Display(Name ="Nombre de Usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Rol")]
        public int? RolId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contraseña")]
        [MinLength(8,ErrorMessage ="El campo {0} debe tener como minino {1} caracter")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$",ErrorMessage ="La contraseña debe tener al menos una Mayuscula, minusculas, digitos y al menos 8 caracteres")]
        public string Password { get; set; } 
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Confirmar Contraseña")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener como minino {1} caracter")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseña debe tener al menos una Mayuscula, minusculas, digitos y al menos 8 caracteres")]
        [Compare("Password", ErrorMessage ="Las contraseñas no son Iguales")]
        public string RePassword { get; set; }
    }
}
