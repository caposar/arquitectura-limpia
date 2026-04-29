using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionDeMediador : Exception
    {
        public ExcepcionDeMediador(string mensaje) : base(mensaje)
        {
        }
    }
}
