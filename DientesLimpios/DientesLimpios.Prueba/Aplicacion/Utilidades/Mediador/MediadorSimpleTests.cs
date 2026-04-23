using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using FluentValidation;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Prueba.Aplicacion.Utilidades.Mediador
{
    [TestClass]
    public class MediadorSimpleTests
    {
        // --- Clases de apoyo para el test ---
        public class RequestFalso : IRequest<string>
        {
            public required string Nombre { get; set; }
        }

        public class HandlerFalso : IRequestHandler<RequestFalso, string>
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("respuesta correcta");
            }
        }

        public class ValidadorRequestFalso : AbstractValidator<RequestFalso>
        {
            public ValidadorRequestFalso()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        // --- Tests ---

        [TestMethod]
        public async Task Send_LlamaMetodoHandler()
        {
            // Arrange
            var request = new RequestFalso() { Nombre = "Nombre A" };
            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.GetService(typeof(IRequestHandler<RequestFalso, string>))
                           .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);

            // Act
            var resultado = await mediador.Send(request);

            // Assert
            await casoDeUsoMock.Received(1).Handle(request);
        }

        [TestMethod]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
        {
            // Arrange
            var request = new RequestFalso() { Nombre = "Nombre A" };
            //var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>(); // No se usa por eso se comentó
            var serviceProvider = Substitute.For<IServiceProvider>();
            var mediador = new MediadorSimple(serviceProvider);

            // Act & Assert
            // Se usa ThrowsAsync porque el método Send es asíncrono
            await Assert.ThrowsAsync<ExcepcionDeMediador>(async () =>
            {
                await mediador.Send(request);
            });
        }

        [TestMethod]
        public async Task Send_ComandoNoValido_LanzaExcepcion()
        {
            // Arrange
            var request = new RequestFalso { Nombre = "" }; // Inválido
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validador = new ValidadorRequestFalso();

            serviceProvider.GetService(typeof(IValidator<RequestFalso>))
                           .Returns(validador);

            var mediador = new MediadorSimple(serviceProvider);

            // Act & Assert
            await Assert.ThrowsAsync<ExcepcionDeValidacion>(async () =>
            {
                await mediador.Send(request);
            });

            // Opcional: Verificar que el mensaje sea el correcto
            // Assert.AreEqual("Mensaje esperado", ex.Message);
        }
    }
}
