using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.ObjetosDeValor
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Email(null!));
        }

        [TestMethod]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Email("felipe.com"));
        }

        [TestMethod]
        public void Constructor_EmailValido_NoLanzaExcepcion()
        {
            // Arrange (Preparar)
            string correoEsperado = "felipe@ejemplo.com";

            // Act (Actuar)
            var email = new Email(correoEsperado);

            // Assert (Afirmar/Comprobar)
            Assert.AreEqual(correoEsperado, email.Valor);
        }
    }
}
