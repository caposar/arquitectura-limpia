using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    public class ConsultaObtenerDetallePaciente : IRequest<PacienteDetalleDTO>
    {
        public required Guid Id { get; set; }
    }
}
