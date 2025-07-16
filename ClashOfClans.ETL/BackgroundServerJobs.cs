using ClashOfClans.ETL.Jobs;
using Quartz.Spi;
using Quartz;

namespace ClashOfClans.ETL;

public static class BackgroundServerJobs
{
    public static void LoadJobs(this BackgroundServer server)
    {
        server.AddClashOfClansJobs();
    }
}