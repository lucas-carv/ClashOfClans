using ClashOfClans.ETL.Models;
using ClashOfClans.ETL.Services;
using Quartz;

namespace ClashOfClans.ETL.Jobs;

public class BuscarMembrosJob : IJob
{
    private readonly ClashOfClansService _clashOfClansService;
    public BuscarMembrosJob(ClashOfClansService clashOfClansService)
    {
        _clashOfClansService = clashOfClansService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        Clan? Clan = await _clashOfClansService.BuscarClan("#2LOUC9R8P");
    }
}
