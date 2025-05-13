using ClashOfClans.ETL.ClashOfClans.Model;
using ClashOfClans.ETL.ClashOfClans.Services;
using ClashOfClans.ETL.Data;
using Google.Protobuf;
using Newtonsoft.Json;
using Quartz;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace ClashOfClans.ETL.ClashOfClans;

public class BuscarGuerrasJob : IJob
{
    private readonly IClanApiService _clanApiService;
    private readonly ClashOfClansRepository _clashOfClansRepository;
    private readonly HttpClient _httpClient;

    public BuscarGuerrasJob(IClanApiService clanApiService, ClashOfClansRepository clashOfClansRepository, HttpClient httpClient)
    {
        _clanApiService = clanApiService;
        _clashOfClansRepository = clashOfClansRepository;
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(33333);
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            Console.WriteLine($"{DateTime.Now} - Executando: {this.GetType().Name}");
            Guerras result = await _clanApiService.ObterGuerras();
            if (result == null)
            {
                Console.WriteLine("Nenhuma guerra encontrada");
                return;
            }

            var historicos = await _clashOfClansRepository.ObterHistorico();
            var historico = historicos.FirstOrDefault(h => h.Recurso == "guerra");

            historico ??= new Historico()
            {
                DataUltimaAlteracao = DateTime.MinValue,
                Recurso = "guerra",
                ReferenciaId = $"{DateTime.MinValue}"
            };

            DateTime parsedDate = DateTime.ParseExact(result.Items.FirstOrDefault().EndTime, "yyyyMMdd'T'HHmmss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            if (parsedDate == historico.DataUltimaAlteracao)
                return;

            await EnviarGuerras(result);
            Console.WriteLine($"{DateTime.Now} - Finalizando: {this.GetType().Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{DateTime.Now} - Erro em {this.GetType().Name}: {ex}");
        }

    }
    public async Task<bool> EnviarGuerras(Guerras guerras)
    {
        if (guerras == null)
            return false;

        var url = ObterUri("guerra");
        var resposta = await _httpClient.PostAsync(
            url,
            ObjetoParaStringContent(guerras));
        return true;
    }
    private Uri ObterUri(string caminhoRecurso)
    {
        ConfigurarCabecalhosClienteHttp();
        var baseUrl = "https://localhost:7242/";
        if (string.IsNullOrEmpty(baseUrl))
            throw new ApplicationException("O destino para envio dos dados não foi identificado!");

        return new Uri($"{baseUrl}api/v1/coc/clan/{caminhoRecurso}");
    }
    private void ConfigurarCabecalhosClienteHttp()
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    private static StringContent ObjetoParaStringContent(object conteudo)
    {
        var configuracoesSerializcao = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
        };

        var objetoSerializado = JsonConvert.SerializeObject(conteudo, configuracoesSerializcao);

        var result = new StringContent(
            objetoSerializado,
            Encoding.UTF8,
            "application/json");

        return result;
    }
}
