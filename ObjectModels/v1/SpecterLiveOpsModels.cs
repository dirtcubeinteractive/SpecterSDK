using System;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v1
{
    public class SpecterSchedule
    {
        public DateTime StartDate;
        public DateTime? EndDate;
        public int? IntervalLength;
        public SPIntervalUnit IntervalUnit;
        public int Occurrences;
        public bool IsRecurring;
    }

    public class SpecterInstanceSchedule : SpecterSchedule
    {
        public SPScheduleStatus Status;
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
    }
}