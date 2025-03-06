using System;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPScheduleData
    {
        public DateTime firstInstanceStartDate { get; set; }
        public DateTime? firstInstanceEndDate { get; set; }
        public string intervalUnit { get; set; } // TODO: Should this be an SPEnum?
        public int intervalLength { get; set; } // TODO: Should this be a 'long' type?
        public int occurrences { get; set; } // TODO: Should this be a 'long' type?
        public bool isRecurring { get; set; }
        public SPInstanceScheduleData currentInstanceSchedule { get; set; }
    }

    [Serializable]
    public class SPInstanceScheduleData
    {
        public string status { get; set; }
        public DateTime instanceStartDate { get; set; }
        public DateTime? instanceEndDate { get; set; }
    }
}