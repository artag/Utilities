using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Core;
using Operations;

namespace AOPlan
{
    class Program
    {
        static void Main(string[] args)
        {
            var checkArgs = ConfigFile.CheckInputArguments(args);
            if (checkArgs.IsFailure)
            {
                Console.WriteLine(checkArgs.Error);
                return;
            }

            var multiplier = checkArgs.Value;

            var checkLinks = ConfigFile.CheckLinksToJsonFiles(args[0]);
            if (checkLinks.IsFailure)
            {
                Console.WriteLine(checkLinks.Error);
                return;
            }

            var daysLoaded = ConfigFile.LoadDaysData(checkLinks.Value);
            if (daysLoaded.IsFailure)
            {
                Console.WriteLine(daysLoaded.Error);
                return;
            }

            checkLinks = ConfigFile.CheckLinksToJsonFiles(args[1]);
            if (checkLinks.IsFailure)
            {
                Console.WriteLine(checkLinks.Error);
                return;
            }

            var jobsLoaded = ConfigFile.LoadJobsData(checkLinks.Value);
            if (jobsLoaded.IsFailure)
            {
                Console.WriteLine(jobsLoaded.Error);
                return;
            }

            var createdDays = CreateDaysFromLoadedData(daysLoaded.Value);
            if (createdDays.IsFailure)
            {
                Console.WriteLine(createdDays.Error);
                return;
            }

            var createdJobs = CreateJobsFromLoadedData(jobsLoaded.Value, multiplier);
            if (createdJobs.IsFailure)
            {
                Console.WriteLine(createdJobs.Error);
                return;
            }

            var days = createdDays.Value;
            var checkSkip = DaysOperations.CheckDaySkip(days);
            if (checkSkip.IsFailure)
            {
                Console.WriteLine(checkSkip.Error);
                return;
            }

            var jobs = createdJobs.Value;
            var jobsToProcess = JobsOperations.GetJobsToProcess(jobs);

            var daysToProcess = new Queue<Day>();
            foreach (var day in days)
            {
                daysToProcess.Enqueue(day);
            }

            foreach (var job in jobsToProcess)
            {
                Process(job, daysToProcess);
            }

            DisplayDays(days);
            DisplayJobs(jobs);
            DisplayProcessedJobs(jobsToProcess);
        }

        private static void DisplayDays(IEnumerable<Day> days)
        {
            Console.WriteLine("Календарь");
            Console.WriteLine("----------");

            foreach (var day in days)
            {
                if (day.Date.DayOfWeek == DayOfWeek.Monday)
                    Console.WriteLine("---");

                Console.WriteLine(day);
            }

            Console.WriteLine();
        }

        private static void DisplayJobs(IEnumerable<Job> jobs)
        {
            Console.WriteLine("Задачи");
            Console.WriteLine("----------");

            foreach (var job in jobs)
            {
                Console.WriteLine(job);
                if (!job.Description.IsNullOrEmpty())
                    Console.WriteLine($"Описание: {job.Description}");
                if (!job.Comment.IsNullOrEmpty())
                    Console.WriteLine($"Комментарий: {job.Comment}");
                Console.WriteLine();

                DisplaySubJobs(job.SubJobs);
                Console.WriteLine("---");
            }

            Console.WriteLine();
        }

        private static void DisplaySubJobs(IEnumerable<Job> subJobs)
        {
            foreach (var subJob in subJobs)
            {
                Console.WriteLine(subJob);

                if (!subJob.Description.IsNullOrEmpty())
                    Console.WriteLine($"Описание: {subJob.Description}");
                if (!subJob.Comment.IsNullOrEmpty())
                    Console.WriteLine($"Комментарий: {subJob.Comment}");
                Console.WriteLine();

                DisplaySubJobs(subJob.SubJobs);
            }
        }

        private static void DisplayProcessedJobs(IEnumerable<Job> processedJobs)
        {
            foreach (var job in processedJobs)
            {
                Console.WriteLine(job);
                foreach (var day in job.Days)
                {
                    var duration = $"{day.Item2:hh}ч {day.Item2:mm}м";
                    Console.WriteLine($"    {day.Item1} {duration}");
                }

                Console.WriteLine();
            }
        }

        private static Result<IEnumerable<Day>> CreateDaysFromLoadedData(
            IEnumerable<JsonDaysData> loadedData)
        {
            var days = new Dictionary<DateTime, Day>();

            foreach (var daysData in loadedData)
            {
                var timeSlots = new List<TimeSlot>();
                foreach (var timeSlotData in daysData.TimeSlots)
                {
                    var createdTimeSlot = TimeSlot.Create(timeSlotData);
                    if (createdTimeSlot.IsFailure)
                        return Result<IEnumerable<Day>>.Fail(createdTimeSlot.Error);

                    timeSlots.Add(createdTimeSlot.Value);
                }

                foreach (var dateData in daysData.Dates)
                {
                    var createdDate = Date.Create(dateData);
                    if (createdDate.IsFailure)
                        return Result<IEnumerable<Day>>.Fail(createdDate.Error);

                    var day = new Day(createdDate.Value, timeSlots);
                    if (days.ContainsKey(day.Date))
                        return Result<IEnumerable<Day>>.Fail($"Day with {day.Date:d} date already exists.");

                    days[day.Date] = day;
                }
            }

            var result = days.Values.ToList();
            result.Sort();

            return Result<IEnumerable<Day>>.Ok(result);
        }

        private static Result<IEnumerable<Job>> CreateJobsFromLoadedData(
            IEnumerable<JsonJobsData> loadedData, double multiplier)
        {
            var jobs = new Dictionary<string, Job>();

            foreach (var data in loadedData)
            {
                foreach (var jobData in data.Jobs)
                {
                    var createdJob = Job.Create(jobData.Id, jobData.ParentId, jobData.DisplayableDescription, jobData.Description, jobData.Comment, jobData.Duration, multiplier);
                    if (createdJob.IsFailure)
                        return Result<IEnumerable<Job>>.Fail(createdJob.Error);

                    var job = createdJob.Value;

                    if (job.ParentId.IsNullOrEmpty())
                    {
                        var id = job.Id;
                        if (jobs.ContainsKey(id))
                            return Result<IEnumerable<Job>>.Fail($"Error. Multiple jobs has id {id}");

                        jobs[id] = job;
                    }
                    else
                    {
                        var parentId = job.ParentId;

                        if (!jobs.ContainsKey(parentId))
                            return Result<IEnumerable<Job>>.Fail($"Can not find job with id {parentId}");

                        var parentJob = jobs[parentId];
                        parentJob.AddSubJob(job);
                    }
                }
            }

            var result = jobs.Values.ToList();
            result.Sort();

            return Result<IEnumerable<Job>>.Ok(result);
        }

        private static void Process(Job job, Queue<Day> days)
        {
            while (job.DurationLeft > TimeSpan.Zero)
            {
                var day = days.Peek();
                if (day.WorkTimeLeft == TimeSpan.Zero)
                {
                    days.Dequeue();
                    continue;
                }

                if (day.WorkTimeLeft <= job.DurationLeft)
                {
                    var durationToSave = day.WorkTimeLeft;

                    job.DurationLeft -= day.WorkTimeLeft;
                    job.AddDay(day, durationToSave);
                    days.Dequeue();
                }
                else
                {
                    var durationToSave = job.DurationLeft;

                    var diff = day.WorkTimeLeft - job.DurationLeft;
                    job.DurationLeft = TimeSpan.Zero;
                    day.WorkTimeLeft = diff;
                    job.AddDay(day, durationToSave);
                }
            }
        }
    }
}
