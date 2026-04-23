using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Persistencia
{
    public interface IUnidadDeTrabajo
    {
        Task Persistir();
        Task Reversar();
    }
}
