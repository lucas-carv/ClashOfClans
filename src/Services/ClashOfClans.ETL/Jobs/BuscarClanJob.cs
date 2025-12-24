using ClashOfClans.ETL.InputModels;
using ClashOfClans.ETL.Models.Clans;
using ClashOfClans.ETL.Services;
using ClashOfClans.ETL.Services.Integration;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

[DisallowConcurrentExecution]
public class BuscarClanJob(ClashOfClansService clashOfClansService) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;

    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        Clan clan = await _clashOfClansService.BuscarClan(encodedTag);
        if (clan is null)
        {
            Console.WriteLine("Falha ao obter clan");
            return;
        }
        IntegrationService integrationService = new();

        CriarClanInputModel clanInputModel = new()
        {
            Tag = clan.Tag,
            Nome = clan.Name,
            Membros = clan.MemberList.Select(m => new MembroDTO()
            {
                Tag = m.Tag,
                Nome = m.Name
            })
        };

        var response = await integrationService.ObterClanPorTag(encodedTag);
        if (!response.IsValid)
        {
            Console.WriteLine($"{DateTime.Now} - Falha ao obter clan \n{string.Join(",", response.Erros)}");
            return;
        }
        if (response.ResponseData is not null)
        {
            Console.WriteLine($"{DateTime.Now} - Atualizando Clan");
            await integrationService.AtualizarClan(clanInputModel);
            return;
        }

        Console.WriteLine($"{DateTime.Now} - Criando Clan");
        await integrationService.CriarClan(clanInputModel);
    }
}
