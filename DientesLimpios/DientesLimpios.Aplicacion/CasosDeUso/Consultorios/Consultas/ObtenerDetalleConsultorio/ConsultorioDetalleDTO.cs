using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class ConsultorioDetalleDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}
