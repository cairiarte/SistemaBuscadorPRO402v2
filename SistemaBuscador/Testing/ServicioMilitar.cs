using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Testing
{
        
    public class ServicioMilitar
    {
        private readonly ICalculaEdad _context;

        public ServicioMilitar(ICalculaEdad context)
        {
            _context = context;
        }
        //fecha nacimiento -Sexo string m o f
        // apta > 18 y M

        public bool EsApto(DateTime fechaNacimiento, string sexo) 
        {
            bool resultado = false;
            
            // Logica del metodo
            if (sexo == "M") 
            {
                //var calculaEdad = new CalculaEdad();
                int edad = _context.CalcularEdad(fechaNacimiento);
                if(edad >= 18) 
                {
                    resultado = true;
                }
            }
            return resultado;
        }
    }
}
 