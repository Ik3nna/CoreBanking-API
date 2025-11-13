public interface IJobMonitoringService

{

    Task<JobStatistics> GetJobStatisticsAsync();

    Task<List<FailedJob>> GetRecentFailedJobsAsync(int count = 50);

    Task<bool> RetryFailedJobAsync(string jobId);

}
