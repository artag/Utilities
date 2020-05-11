using System.Collections.Generic;
using System.Linq;
using Core;

namespace Operations
{
    public static class JobsOperations
    {
        public static IEnumerable<Job> GetJobsToProcess(IEnumerable<Job> jobs)
        {
            var jobToProcess = new List<Job>();
            foreach (var job in jobs)
            {
                if (!job.SubJobs.Any())
                    jobToProcess.Add(job);

                var subJobsToProcess = GetJobsToProcess(job.SubJobs);
                jobToProcess.AddRange(subJobsToProcess);
            }

            return jobToProcess;
        }
    }
}
