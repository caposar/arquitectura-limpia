using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CancelarCita
{
    public class ComandoCancelarCita : IRequest
    {
        public required Guid Id { get; set; }
    }
}
