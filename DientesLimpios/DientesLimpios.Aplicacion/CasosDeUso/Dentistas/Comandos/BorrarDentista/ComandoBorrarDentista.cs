using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.BorrarDentista
{
    public class ComandoBorrarDentista : IRequest
    {
        public Guid Id { get; set; }
    }
}
