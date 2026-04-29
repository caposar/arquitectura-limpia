using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoCrearPacienteTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioPacientes repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoCrearPaciente casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoDatosValidos_CreaPacienteYPersisteYRetornaId()
        {
            // Arrange (Preparación)
            var comando = new ComandoCrearPaciente { Nombre = "Felipe", Email = "felipe@ejemplo.com" };
            var pacienteCreado = new Paciente(comando.Nombre, new Email(comando.Email));
            var id = pacienteCreado.Id;

            repositorio.Agregar(Arg.Any<Paciente>()).Returns(pacienteCreado);

            // Act (Ejecución)
            var idResultado = await casoDeUso.Handle(comando);

            // Assert (Verificación)
            Assert.AreEqual(id, idResultado);
            await repositorio.Received(1).Agregar(Arg.Any<Paciente>());
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            // Arrange
            var comando = new ComandoCrearPaciente { Nombre = "Felipe", Email = "felipe@ejemplo.com" };

            // Usamos ThrowsAsync simulando el comportamiento real de un Task fallido en la BD
            repositorio.Agregar(Arg.Any<Paciente>()).ThrowsAsync(new InvalidOperationException("Error al insertar"));

            // Act & Assert 1: Validamos que la excepción se propague hacia arriba
            await Assert.ThrowsAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));

            // Assert 2: Validamos los efectos secundarios en la transacción
            await unidadDeTrabajo.Received(1).Reversar();
            await unidadDeTrabajo.DidNotReceive().Persistir();
        }
    }
}
