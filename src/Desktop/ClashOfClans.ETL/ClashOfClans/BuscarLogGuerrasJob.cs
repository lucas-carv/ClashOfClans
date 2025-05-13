using ClashOfClans.ETL.ClashOfClans.Model;
using ClashOfClans.ETL.ClashOfClans.Services;
using Quartz;
using System.Globalization;
using System.Reflection;

namespace ClashOfClans.ETL.ClashOfClans
{
    public class BuscarGuerrasJob : IJob
    {
        private readonly IClanApiService _clanApiService;

        public BuscarGuerrasJob(IClanApiService clanApiService)
        {
            _clanApiService = clanApiService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now} - Executando: {this.GetType().Name}");
                var result = await _clanApiService.ObterGuerras();

                //var historicos = ObterHistoricoIntegracao();
                //var historico = historicos.First(h => h.Recurso == "guerra");

                foreach (Item item in result.Items)
                {

                }

                var teste = result.Items.OrderByDescending(g => g.EndTime).FirstOrDefault();
                var teste2 = result.Items.OrderBy(g => g.EndTime).FirstOrDefault();
                DateTime date1 = DateTime.ParseExact(teste.EndTime, "yyyyMMdd'T'HHmmss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                DateTime date2 = DateTime.ParseExact(teste2.EndTime, "yyyyMMdd'T'HHmmss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

                Console.WriteLine($"{DateTime.Now} - Finalizando: {this.GetType().Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - Erro em {this.GetType().Name}: {ex}");
            }

        }
    }
}
