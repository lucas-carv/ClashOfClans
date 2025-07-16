namespace ClashOfClans.ETL.Jobs;

public static class ClashOfClansJobs
{
    public static void AddClashOfClansJobs(this BackgroundServer configuration)
    {
        configuration.AddJob<BuscarMembrosJob>(IntervalSimpleSchedule.Minutes, 5);
    }
}
