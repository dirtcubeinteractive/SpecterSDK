using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.APIModels.AdminModels
{
    public enum SPParamDataType
    {
        String = 1,
        Integer,
        Boolean,
        Float,
        DateTime,
        Logical
    }
    
    public enum SPAppEventType
    {
        Default,
        Custom
    }
    
    [Serializable]
    public class SPAppEvent
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<SPAppEventParameter> defaultParameterDetails { get; set; }
        public List<SPAppEventParameter> customParameterDetails { get; set; }
        
        [JsonIgnore] public string type { get; set; }
        [JsonIgnore] public List<SPAppEventParameter> allParameters { get; private set; }
        public List<SPAppEventParameter> GetAllParameters()
        {
            if (allParameters == null)
            {
                allParameters = new List<SPAppEventParameter>(defaultParameterDetails ?? new List<SPAppEventParameter>());
                if (customParameterDetails != null && customParameterDetails.Count > 0)
                    allParameters.AddRange(customParameterDetails);
            }
            
            return allParameters;
        }
    }

    [Serializable]
    public class SPAppEventParameter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public SPParamDataType dataTypeId { get; set; }
    }

    [Serializable]
    public abstract class SPGetAppEventsAdminRequest : SPApiRequestBase
    {
        public List<string> ids { get; set; }
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    [Serializable]
    public class SPGetAppEventsAdminResponseData : ISpecterApiResponseData
    {
        public List<SPAppEvent> appEventDetails { get; set; }
    }
    
    [Serializable]
    public class SPGetDefaultEventsAdminRequest : SPGetAppEventsAdminRequest { }

    public class SPGetDefaultEventsAdminResult : SpecterApiResultBase<SPGetAppEventsAdminResponseData>
    {
        public List<SPAppEvent> AppEventDetails;

        protected override void InitSpecterObjectsInternal()
        {
            AppEventDetails = Response.data.appEventDetails;
        }
    }

    [Serializable]
    public class SPGetCustomEventsAdminRequest : SPGetAppEventsAdminRequest { }

    public class SPGetCustomEventsAdminResult : SpecterApiResultBase<SPGetAppEventsAdminResponseData>
    {
        public List<SPAppEvent> AppEventDetails;

        protected override void InitSpecterObjectsInternal()
        {
            AppEventDetails = Response.data.appEventDetails;
        }
    }
}