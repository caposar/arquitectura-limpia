using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTests
    {
        [TestMethod]
        public void Constructor_FechaInicioPosteriorAFechaFin_LanzaExcepcion()
        {
            // Arrange
            var inicio = DateTime.UtcNow;
            var fin = inicio.AddDays(-1);

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() =>
                new IntervaloDeTiempo(inicio, fin)
            );
        }

        [TestMethod]
        public void Constructor_ParametrosCorrectos_NoLanzaExcepcion()
        {
            // Arrange
            var inicio = DateTime.UtcNow;
            var fin = inicio.AddMinutes(30);

            // Act
            var intervalo = new IntervaloDeTiempo(inicio, fin);

            // Assert
            Assert.AreEqual(inicio, intervalo.Inicio);
            Assert.AreEqual(fin, intervalo.Fin);
        }
    }
}
