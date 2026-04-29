using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class ValidadorComandoCrearCita : AbstractValidator<ComandoCrearCita>
    {
        public ValidadorComandoCrearCita()
        {
            RuleFor(x => x.FechaInicio)
                .GreaterThan(DateTime.UtcNow).WithMessage("La fecha inicio no puede estar en el pasado");

            RuleFor(x => x.FechaFin)
                .GreaterThan(x => x.FechaInicio).WithMessage("La fecha fin debe ser posterior a la fecha de inicio");
        }
    }
}
