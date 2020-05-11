using System;
using System.Collections.Generic;
using System.Linq;
using Common;

using Math = Core.Operations.Math;

namespace Core
{
    public class Job : IComparable<Job>
    {
        private static readonly int NearestMinutesToRound = 15;

        private readonly List<Job> _subJobs = new List<Job>();
        private readonly List<(Day, TimeSpan)> _days = new List<(Day, TimeSpan)>();

        private Job(string id, string parentId, string displayableDescription, string description, string comment, TimeSpan duration)
        {
            Id = id;
            ParentId = parentId;
            DisplayableDescription = displayableDescription;
            Description = description;
            Comment = comment;
            Duration = duration;

            DurationLeft = Duration;
        }

        public string Id { get; }

        public string ParentId { get; }

        public string DisplayableDescription { get; }

        public string Description { get; }

        public string Comment { get; }

        public TimeSpan Duration { get; private set; }

        public TimeSpan DurationLeft { get; set; }

        public IEnumerable<Job> SubJobs => _subJobs;

        public IEnumerable<(Day, TimeSpan)> Days => _days;

        public static Result<Job> Create(
            string id, string parentId, string displayableDescription, string description, string comment, string duration, double multiplier)
        {
            var durationParsed = Parser.ParseTimeSpan(duration);
            if (durationParsed.IsFailure)
                return Result<Job>.Fail(durationParsed.Error);

            var jobDuration = durationParsed.Value.First();
            var multipliedDuration = Math.MultiplyTimeSpan(jobDuration, multiplier);
            var roundedDuration = Math.RoundTimeSpanToNearestMinutes(
                multipliedDuration, NearestMinutesToRound);

            var job = new Job(
                id, parentId, displayableDescription, description, comment, roundedDuration);

            return Result<Job>.Ok(job);
        }

        /// <inheritdoc />
        public int CompareTo(Job other)
        {
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }

        public void AddSubJob(Job subJob)
        {
            _subJobs.Add(subJob);
            RefreshDuration();
        }

        public void AddDay(Day day, TimeSpan duration)
        {
            _days.Add((day, duration));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Id} {DisplayableDescription} ({Duration:dd}д {Duration:hh}ч {Duration:mm}м)";
        }

        private void RefreshDuration()
        {
            if (!SubJobs.Any())
                return;

            Duration = TimeSpan.Zero;

            foreach (var subJob in SubJobs)
            {
                Duration += subJob.Duration;
                subJob.RefreshDuration();
            }

            DurationLeft = Duration;
        }
    }
}
