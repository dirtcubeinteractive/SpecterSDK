using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPPlayerProfile
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
        
        public SPPlayerProfile() { }
        public SPPlayerProfile(SPPlayerProfileData data)
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