using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK.Models
{
    [System.Serializable]
    public class SPAuthUserAccount
    {
        public string authProvider { get; set; }
        public string userId { get; set; }
    }
    
    [System.Serializable]
    public class SPUserProfile
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string hash { get; set; }
        public string thumbUrl { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string accessToken { get; set; }
        public string entityToken { get; set; }
        public List<SPAuthUserAccount> linkedAccounts { get; set; }
    }
}
