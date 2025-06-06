using System;
using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;

namespace SpecterSDK.ObjectModels
{
    public class SpecterUserBase : SpecterObject
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThumbUrl { get; set; }

        public SpecterUserBase(SPUserProfileResponseBaseData data) : base(data.uuid, data.id)
        {
            Username = data.username;
            DisplayName = data.displayName;
            FirstName = data.firstName;
            LastName = data.lastName;
            ThumbUrl = data.thumbUrl;
        }
    }
    
    public class SpecterUser : SpecterUserBase
    {
        public string Hash;
        public string Email;
        public string Phone;
        public readonly List<SPAuthAccount> LinkedAccounts;

        public SpecterUser(SPUserProfileResponseData data) : base(data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Username = data.username; 
            Hash = data.hash;
            ThumbUrl = data.thumbUrl;
            Email = data.email; 
            Phone = data.phone;
            
            LinkedAccounts = new List<SPAuthAccount>();
            foreach (var account in data.linkedAccounts)
            {
                LinkedAccounts.Add(new SPAuthAccount() { AuthProvider = account.authProvider, UserId = account.userId });
            }
        }
    }

    public class SPAuthAccount
    {
        public string AuthProvider;
        public string UserId;
    }
}