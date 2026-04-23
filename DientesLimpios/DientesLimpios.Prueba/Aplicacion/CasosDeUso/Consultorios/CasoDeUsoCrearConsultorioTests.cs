using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            // Arrange
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            var consultorioCreado = new Consultorio("Consultorio A");

            // Configuramos el mock para que devuelva el objeto creado
            repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            // Act
            var resultado = await casoDeUso.Handle(comando);

            // Assert
            await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            // Arrange
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var resultado = await casoDeUso.Handle(comando);
            });

            // Verificamos que, ante el error, se llamó a Reversar (Rollback)
            await unidadDeTrabajo.Received(1).Reversar();

        }
    }
}
