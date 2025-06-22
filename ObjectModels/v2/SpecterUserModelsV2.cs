using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPPlayerProfile : ISpecterUserProfile
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ThumbUrl { get; set; }
        public string Email { get; set; }
        public string ReferralCode { get; set; }
        
        public SPPlayerProfile() { }
        public SPPlayerProfile(ISpecterUserProfileData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            FirstName = data.firstName;
            LastName = data.lastName;
            DisplayName = data.displayName;
            Username = data.username;
            ThumbUrl = data.thumbUrl;
            Email = data.email;
            ReferralCode = data.referralCode;
        }
    }
    
    public class SPMyPlayerProfile : SPPlayerProfile
    {
        public List<SPUserAuthAccount> LinkedAccounts { get; set; }
        public List<SPInventoryItem> EquippedItems { get; set; }
        
        public SPMyPlayerProfile() : base() { }
        public SPMyPlayerProfile(SPMyProfileData data) : base(data)
        {
            Uuid = data.uuid;
            Id = data.id;
            FirstName = data.firstName;
            LastName = data.lastName;
            DisplayName = data.displayName;
            Username = data.username;
            ThumbUrl = data.thumbUrl;
            Email = data.email;
            ReferralCode = data.referralCode;
            
            LinkedAccounts = data.linkedAccounts?.ConvertAll(x => new SPUserAuthAccount(x));
            EquippedItems = data.equippedItems?.ConvertAll(x => new SPInventoryItem(x));
        }
    }
    
    public class SPBasePlayerProfile : ISpecterBaseUserProfile
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string ThumbUrl { get; set; }
        
        public string CustomId { get; set; }
        public string Email { get; set; }
        
        public SPBasePlayerProfile() { }
        public SPBasePlayerProfile(SPPlayerProfileData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            FirstName = data.firstName;
            LastName = data.lastName;
            DisplayName = data.displayName;
            Username = data.username;
            ThumbUrl = data.thumbUrl;
            CustomId = data.customId;
            Email = data.email;
        }
    }

    public class SPPlayerDataEntry
    {
        public object Value { get; set; }
        public SPPlayerDataPermission Permission { get; set; }
        
        public SPPlayerDataEntry() { }
        public SPPlayerDataEntry(SPPlayerDataEntryData data)
        {
            Value = data.value;
            Permission = data.permission;
        }
        
        public bool TryConvertValue<T>(out T val)
        {
            return SpecterJson.TryConvertObject<T>(Value, out val);
        }
    }
}