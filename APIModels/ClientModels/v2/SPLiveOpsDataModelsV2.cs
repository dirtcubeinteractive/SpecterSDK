using System;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPScheduleData
    {
        /// <summary>
        /// The start date of the schedule.
        /// </summary>
        public DateTime firstInstanceStartDate { get; set; }
        
        /// <summary>
        /// The end date of only the first occurence of the schedule.
        /// In a non-recurring schedule, this will be the end date of the entire schedule.
        /// </summary>
        public DateTime? firstInstanceEndDate { get; set; }
        
        /// <summary>
        /// The unit of time for the length of each occurrence of the schedule.
        /// </summary>
        public SPIntervalUnit intervalUnit { get; set; }
        
        /// <summary>
        /// The length of each occurrence of the schedule.
        /// </summary>
        public long intervalLength { get; set; }
        
        /// <summary>
        /// The number of occurrences of the schedule.
        /// </summary>
        public long occurrences { get; set; }
        
        /// <summary>
        /// Flag indicating whether the schedule is recurring.
        /// </summary>
        public bool isRecurring { get; set; }
        
        /// <summary>
        /// Information about the current instance of the schedule.
        /// </summary>
        public SPInstanceScheduleData currentInstanceSchedule { get; set; }
    }

    [Serializable]
    public class SPInstanceScheduleData
    {
        /// <summary>
        /// The status of the specified instance. For tasks this can be compared against values found in
        /// <see cref="SPTasksScheduleStatus"/> and for competitions values can be compared against <see cref="SPScheduleStatus"/> values.
        /// </summary>
        public string status { get; set; }
        
        /// <summary>
        /// The start date of the instance.
        /// </summary>
        public DateTime instanceStartDate { get; set; }
        
        /// <summary>
        /// The end date of the instance.
        /// </summary>
        public DateTime? instanceEndDate { get; set; }
    }
}