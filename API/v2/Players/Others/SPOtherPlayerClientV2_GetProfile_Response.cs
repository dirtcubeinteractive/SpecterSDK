using System;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPGetOtherPlayerProfileResponse : ISpecterApiResponseData
    {
        public SPPlayerProfileData user { get; set; }
    }

    [Serializable]
    public class SPPlayerProfileData : ISpecterUserProfileData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string username { get; set; }
        public string thumbUrl { get; set; }
        public string email { get; set; }
        public string referralCode { get; set; }
    }
}