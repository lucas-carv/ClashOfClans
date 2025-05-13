namespace ClashOfClans.ETL.ClashOfClans;

public static class ClashOfClansJobs
{
    public static void AddClashOfClansJobs(this BackgroundServer configuration)
    {
        const string queue = "clash_of_clans";

        configuration.AddJob<BuscarGuerrasJob>(IntervalSimpleSchedule.Minutes, 12, queue);
    }
}