using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Configuraciones
{
    public class CitaConfig : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ComplexProperty(prop => prop.IntervaloDeTiempo, accion =>
            {
                accion.Property(e => e.Inicio).HasColumnName("FechaInicio");
                accion.Property(e => e.Fin).HasColumnName("FechaFin");
            });
        }
    }
}
