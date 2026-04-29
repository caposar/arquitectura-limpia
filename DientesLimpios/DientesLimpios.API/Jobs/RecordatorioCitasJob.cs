using DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.EnviarRecordatorioCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    public class RecordatorioCitasJob : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        //private readonly TimeZoneInfo zonaHorariaRD = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        private readonly TimeZoneInfo zonaHorariaAR = TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires");

        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var ahora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHorariaAR);

                if (ahora.Hour == 8)
                {
                    using var scope = scopeFactory.CreateScope();
                    var mediador = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediador.Send(new ComandoEnviarRecordatorioCitas());
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
