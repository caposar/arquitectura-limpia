using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    public class ConsultaObtenerDetalleDentista : IRequest<DentistaDTO>
    {
        public required Guid Id { get; set; }
    }
}
