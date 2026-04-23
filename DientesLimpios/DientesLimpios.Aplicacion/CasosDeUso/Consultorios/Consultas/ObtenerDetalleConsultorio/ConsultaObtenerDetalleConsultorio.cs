using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class ConsultaObtenerDetalleConsultorio : IRequest<ConsultorioDetalleDTO>
    {
        public Guid Id { get; set; }
    }
}
