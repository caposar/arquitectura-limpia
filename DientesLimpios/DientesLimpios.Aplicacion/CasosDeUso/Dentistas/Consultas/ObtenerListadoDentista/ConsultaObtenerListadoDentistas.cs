using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentista
{
    public class ConsultaObtenerListadoDentistas : FiltroDentistasDTO, IRequest<PaginadoDTO<DentistaListadoDTO>>
    {
    }
}
