using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class ComandoCrearPaciente : IRequest<Guid>
    {
        public required string Nombre { get; set; }
        public required string Email { get; set; }
    }
}
