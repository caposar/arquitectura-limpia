using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Notificaciones
{
    public interface IServicioNotificaciones
    {
        Task EnviarConfirmacionCita(ConfirmacionCitaDTO cita);
        Task EnviarRecordatorioCita(RecordatorioCitaDTO cita);
    }
}
