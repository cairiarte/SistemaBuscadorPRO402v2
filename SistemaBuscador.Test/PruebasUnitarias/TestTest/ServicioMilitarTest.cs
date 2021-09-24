using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBuscador.Test.PruebasUnitarias.TestTest
{
    [TestClass]
    public class ServicioMilitarTest
    {
        
        [TestMethod]
        public void NoEsValidoPorSexo() 
        {
            //Preparacion
            string sexo = "F";
            DateTime fechaNac = new DateTime(2000, 12, 1);
            var clase = new ServicioMilitar(new CalculaEdadMock());

            //Ejecucion
            bool resultado = clase.EsApto(fechaNac, sexo);

            //Verificacion;
            Assert.IsFalse(resultado);
        }
    }
}
