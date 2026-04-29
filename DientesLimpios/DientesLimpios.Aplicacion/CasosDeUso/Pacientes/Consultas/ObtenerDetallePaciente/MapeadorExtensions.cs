using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    public static class MapeadorExtensions
    {
        public static PacienteDetalleDTO ADto(this Paciente paciente)
        {
            var dto = new PacienteDetalleDTO
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Email = paciente.Email.Valor
            };
            return dto;
        }
    }
}
