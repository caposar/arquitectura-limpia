using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.Entidades
{
    [TestClass]
    public class ConsultorioTests
    {
        [TestMethod]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Consultorio(null!));
        }

        [TestMethod]
        public void Constructor_DatosValidos_CreaConsultorioCorrectamente()
        {
            // Arrange
            var nombre = "Consultorio Central";

            // Act
            var consultorio = new Consultorio(nombre);

            // Assert
            Assert.AreEqual(nombre, consultorio.Nombre);
            Assert.AreNotEqual(Guid.Empty, consultorio.Id); // Verifica que el Guid v7 se generó
        }
    }
}
