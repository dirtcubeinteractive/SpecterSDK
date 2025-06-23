using System;
using System.Collections.Generic;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    // TODO: Consolidate this with SPMyPlayerProfile once fields are finalized.
    [Serializable]
    public class SPAuthenticatedUserData : ISpecterUserProfileData
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
        
        public bool? isKycComplete { get; set; }
        
        public List<SPUserAuthAccountDataV2> linkedAccounts { get; set; }
    }
}