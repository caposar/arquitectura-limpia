using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CompletarCita
{
    public class ComandoCompletarCita : IRequest
    {
        public required Guid Id { get; set; }
    }
}
