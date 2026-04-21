using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.Entidades
{
    [TestClass]
    public class PacienteTests
    {
        [TestMethod]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("felipe@ejemplo.com");

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Paciente(null!, email));
        }

        [TestMethod]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Paciente("Felipe", email));
        }

        [TestMethod]
        public void Constructor_DatosValidos_CreaPaciente()
        {
            // Arrange
            var email = new Email("felipe@ejemplo.com");
            var nombre = "Felipe";

            // Act
            var paciente = new Paciente(nombre, email);

            // Assert
            Assert.IsNotNull(paciente);
            Assert.AreEqual(nombre, paciente.Nombre);
        }
    }
}
