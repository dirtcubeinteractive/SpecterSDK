using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.API;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.AdminModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.Editor.API
{
    [Serializable]
    public class SPGetProgressionSystemsAdminRequest : SPApiRequestBaseData, IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; }
    }

    [Serializable]
    public class SPGetProgressionSystemsAdminResponseData : ISpecterApiResponseData
    {
        public List<SPProgressionSystemAdminData> levelDetails;
    }

    public class SPGetProgressionSystemsAdminResult : SpecterApiResultBase<SPGetProgressionSystemsAdminResponseData>
    {
        public List<SPProgressionSystemAdminData> LevelDetails;

        protected override void InitSpecterObjectsInternal()
        {
            LevelDetails = Response.data.levelDetails;
        }
    }

    [Serializable]
    public class SPGetTaskListAdminRequest : SPApiRequestBaseData, IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; } = new();
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    public class SPGetTaskListAdminResult : SpecterApiResultBase<SPResponseDataList<SPTaskAdminData>>
    {
        public List<SPTaskAdminData> TaskList;

        protected override void InitSpecterObjectsInternal()
        {
            TaskList = Response.data;
        }
    }

    public class SPEditorApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPEditorApiClient(SpecterRuntimeConfig config) : base(config) {  }

        public async Task<SPGetDefaultEventsAdminResult> GetDefaultEvents(SPGetDefaultEventsAdminRequest request)
        {
            var result = await PostAsync<SPGetDefaultEventsAdminResult, SPGetAppEventsAdminResponseData>("/v1/app-event/get/default", AuthType, request);
            return result;
        }
        
        public async Task<SPGetCustomEventsAdminResult> GetCustomEvents(SPGetCustomEventsAdminRequest request)
        {
            var result = await PostAsync<SPGetCustomEventsAdminResult, SPGetAppEventsAdminResponseData>("/v1/app-event/get/custom", AuthType, request);
            return result;
        }

        public async Task<List<SPAppEvent>> GetEvents()
        {
            var ConstructEvents = new Func<SPAppEventType, List<SPAppEvent>, List<SPAppEvent>, List<SPAppEvent>>((type, unionEvents, events) =>
            {
                foreach (var appEvent in events)
                    appEvent.type = type.ToString().ToLower();
                unionEvents.AddRange(events);
                return unionEvents;
            });

            var customEventsResult = await GetCustomEvents(new SPGetCustomEventsAdminRequest());
            var defaultEventsResult = await GetDefaultEvents(new SPGetDefaultEventsAdminRequest());

            if (customEventsResult == null || defaultEventsResult == null)
                return new List<SPAppEvent>();
                
            var allEvents = new List<SPAppEvent>();
            allEvents = ConstructEvents(SPAppEventType.Custom, allEvents, customEventsResult.AppEventDetails);
            allEvents = ConstructEvents(SPAppEventType.Default, allEvents, defaultEventsResult.AppEventDetails);

            return allEvents;
        }

        public async Task<SPGetTaskListAdminResult> GetTaskList(SPGetTaskListAdminRequest request)
        {
            var result = await PostAsync<SPGetTaskListAdminResult, SPResponseDataList<SPTaskAdminData>>("/v1/task/get", AuthType, request);
            return result;
        }

        public async Task<SPGeneralResult> CreateTask(SPCreateTaskAdminRequest request)
        {
            var result = await PostAsync<SPGeneralResult, SPGeneralResponseDictionaryData>("/v1/task/create", AuthType, request);
            return result;
        }

        public async Task<SPGetProgressionSystemsAdminResult> GetProgressionSystems(SPGetProgressionSystemsAdminRequest request)
        {
            var result = await PostAsync<SPGetProgressionSystemsAdminResult, SPGetProgressionSystemsAdminResponseData>("/v1/level-system/get", AuthType, request);
            return result;
        }
    }
}