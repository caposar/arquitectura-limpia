using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerDetalleCita
{
    public class ConsultaObtenerDetalleCita : IRequest<CitaDetalleDTO>
    {
        public required Guid Id { get; set; }
    }
}
