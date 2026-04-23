using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class ComandoCrearConsultorio : IRequest<Guid>
    {
        public required string Nombre { get; set; }
    }
}
