using ClashOfClans.ETL.ClashOfClans;

namespace ClashOfClans.ETL;

public static class BackgroundServerJobs
{
    public static void LoadJobs(this BackgroundServer server)
    {
        server.AddClashOfClansJobs();
    }
}
