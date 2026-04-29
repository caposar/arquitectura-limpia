using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentista;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioDentista : IRepositorio<Dentista>
    {
        Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistasDTO filtro);
    }
}
