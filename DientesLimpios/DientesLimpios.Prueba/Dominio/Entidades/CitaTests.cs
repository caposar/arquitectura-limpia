using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Dominio.Entidades
{
    [TestClass]
    public class CitaTests
    {
        // Usamos campos privados para no repetir datos en cada test (Arrange global)
        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(2));

        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            // Arrange (Preparar) - Usando los campos de la clase

            // Act
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);

            // Assert (Verificar) - VALIDACIÓN COMPLETA DE MAPEO
            Assert.AreEqual(_pacienteId, cita.PacienteId);
            Assert.AreEqual(_dentistaId, cita.DentistaId);
            Assert.AreEqual(_consultorioId, cita.ConsultorioId);
            Assert.AreEqual(_intervalo, cita.IntervaloDeTiempo);
            Assert.AreEqual(EstadoCita.Programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        [TestMethod]
        public void Constructor_FechaInicioEnElPasado_LanzaExcepcion()
        {
            // Arrange
            var intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
            
            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => 
                new Cita(_pacienteId, _dentistaId, _consultorioId, intervalo));
        }

        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);

            // Act
            cita.Cancelar();

            // Assert
            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }

        [TestMethod]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar(); // Ya está cancelada

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => cita.Cancelar());
        }

        [TestMethod]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);

            // Act
            cita.Completar();

            // Assert
            Assert.AreEqual(EstadoCita.Completada, cita.Estado);
        }

        [TestMethod]
        public void Completar_CitaCancelada_LanzaExcepcion()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Cancelar(); // Ya está cancelada

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => cita.Completar());
        }

        [TestMethod]
        public void Completar_CitaCompletada_LanzaExcepcion()
        {
            // Arrange
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
            cita.Completar(); // Ya está completada

            // Act & Assert
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => cita.Completar());
        }
    }
}
