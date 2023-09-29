using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.ObjectModels
{
    public class SpecterUser : SpecterObject //SPObjectBase<SpecterUser, SPUserProfileResponseData>
    {
        public string Uuid;
        public string Id;
        public string Username;
        public string Hash;
        public string ThumbUrl;
        public string Email;
        public string Phone;
        public SPAuthContext AuthContext;
        public List<SPAuthAccount> LinkedAccounts;

        public SpecterUser() { }

        public SpecterUser(SPUserProfileResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Username = data.username; 
            Hash = data.hash;
            ThumbUrl = data.thumbUrl;
            Email = data.email; 
            Phone = data.phone;

            AuthContext = new SPAuthContext() { AccessToken = data.accessToken, EntityToken = data.entityToken };
            
            LinkedAccounts = new List<SPAuthAccount>();
            foreach (var account in data.linkedAccounts)
            {
                LinkedAccounts.Add(new SPAuthAccount() { AuthProvider = account.authProvider, UserId = account.userId });
            }
        }
    }

    public class SPAuthContext
    {
        public string AccessToken;
        public string EntityToken;
    }
    
    public class SPAuthAccount
    {
        public string AuthProvider;
        public string UserId;
    }
}