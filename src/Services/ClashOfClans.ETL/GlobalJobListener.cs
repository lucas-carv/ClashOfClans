using Quartz;

namespace ClashOfClans.ETL;

public class GlobalJobListener : IJobListener
{

    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        //LogService.GravarLog("quartz-log.txt", string.Format("{0} -- {1} -- Job ({2}) is about to be executed", DateTime.Now, Name, context.JobDetail.Key));

        return Task.CompletedTask;
    }

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        //LogService.GravarLog("quartz-log.txt", string.Format("{0} -- {1} -- Job ({2}) was vetoed", DateTime.Now, Name, context.JobDetail.Key));
        return Task.CompletedTask;
    }

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
    {
        //LogService.GravarLog("quartz-log.txt", string.Format("{0} -- {1} -- Job ({2}) was executed", DateTime.Now, Name, context.JobDetail.Key));
        return Task.CompletedTask;
    }

    public string Name { get { return "GlobalJobListener"; } }
}
