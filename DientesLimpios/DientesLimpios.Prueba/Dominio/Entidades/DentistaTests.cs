using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.Entidades
{
    [TestClass]
    public class DentistaTests
    {
        [TestMethod]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("felipe@ejemplo.com");

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Dentista(null!, email));
        }

        [TestMethod]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            
            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Dentista("Felipe", email));
        }

        [TestMethod]
        public void Constructor_DatosValidos_CreaDentistaCorrectamente()
        {
            // Arrange
            var nombre = "Dr. Felipe";
            var email = new Email("felipe@ejemplo.com");

            // Act
            var dentista = new Dentista(nombre, email);

            // Assert
            Assert.IsNotNull(dentista);
            Assert.AreEqual(nombre, dentista.Nombre);
            Assert.AreEqual(email, dentista.Email);
            Assert.AreNotEqual(Guid.Empty, dentista.Id); // Muy importante para validar tu Guid v7
        }
    }
}
