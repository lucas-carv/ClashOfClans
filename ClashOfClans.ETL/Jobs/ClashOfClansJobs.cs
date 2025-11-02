namespace ClashOfClans.ETL.Jobs;

public static class ClashOfClansJobs
{
    public static void AddClashOfClansJobs(this BackgroundServer configuration)
    {
        configuration.AddJob<BuscarClanJob>(IntervalSimpleSchedule.Minutes, 5);
        configuration.AddJob<EnviarGuerraJob>(IntervalSimpleSchedule.Minutes, 15);
    }
}
