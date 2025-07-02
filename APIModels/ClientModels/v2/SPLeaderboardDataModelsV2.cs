using System;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPCompetitorProfileData : ISpecterBaseUserProfileData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string username { get; set; }
        public string thumbUrl { get; set; }
    }
}