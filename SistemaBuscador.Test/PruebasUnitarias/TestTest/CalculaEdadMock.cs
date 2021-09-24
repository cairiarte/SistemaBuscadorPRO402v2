using SistemaBuscador.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBuscador.Test.PruebasUnitarias.TestTest
{
    public class CalculaEdadMock : ICalculaEdad
    {
        public int CalcularEdad(DateTime fechaNacimiento)
        {
            return 18;
        }
    }
}
