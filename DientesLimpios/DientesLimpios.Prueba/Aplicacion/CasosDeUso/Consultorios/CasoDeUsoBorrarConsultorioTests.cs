using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoBorrarConsultorioTests
    {
        private IRepositorioConsultorios repositorio = null!;
        private IUnidadDeTrabajo unidadDeTrabajo = null!;
        private CasoDeUsoBorrarConsultorio casoDeUso = null!;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoBorrarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
            // Arrange
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Act
            await casoDeUso.Handle(comando);

            // Assert
            await repositorio.Received(1).Borrar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            // Arrange
            var comando = new ComandoBorrarConsultorio { Id = Guid.NewGuid() };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            // Act & Assert
            await Assert.ThrowsAsync<ExcepcionNoEncontrado>(() => casoDeUso.Handle(comando));
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamaAReversarYLanzaExcepcion()
        {
            // Arrange
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Usamos ThrowsAsync simulando el comportamiento real de un Task fallido
            repositorio.Borrar(consultorio).ThrowsAsync(new InvalidOperationException("Fallo al borrar"));

            // Act & Assert 1: Validamos que la excepción se propaga hacia arriba
            await Assert.ThrowsAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            // Assert 2: Validamos el efecto secundario del rollback
            await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
