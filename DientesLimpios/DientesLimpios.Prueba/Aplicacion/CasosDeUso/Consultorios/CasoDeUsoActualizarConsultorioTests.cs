using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
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
    public class CasoDeUsoActualizarConsultorioTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoActualizarConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizaNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo nombre" };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            await repositorio.Received(1).Actualizar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            // Arrange
            var comando = new ComandoActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nombre" };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            // Act & Assert
            await Assert.ThrowsAsync<ExcepcionNoEncontrado>(async () => await casoDeUso.Handle(comando));
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlActualizar_LlamaAReversarYLanzaExcepcion()
        {
            // Arrange
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Consultorio B" };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Usamos ThrowsAsync de NSubstitute.ExceptionExtensions para métodos que devuelven Task
            repositorio.Actualizar(consultorio).ThrowsAsync(new InvalidOperationException("Error al actualizar"));

            // Act & Assert 1: Verificamos que el caso de uso deje pasar la excepción hacia arriba
            await Assert.ThrowsAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            // Assert 2: Verificamos el efecto secundario (que se haya hecho el rollback)
            await unidadDeTrabajo.Received(1).Reversar();
        }


    }
}
