using System;
using System.Collections.Generic;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.ObjectModels.Interfaces
{
    public interface ISpecterObject { }
    public interface ISpecterRewardable { }

    public interface ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        public bool TryGetMeta<T>(string key, out T val)
        {
            bool success = false;
            val = default;
            
            try
            {
                if (Meta.TryGetValue(key, out string str))
                {
                    val = SpecterJson.DeserializeObject<T>(str);
                    success = true;
                }
                else
                    Debug.LogError($"No meta data with key {key} exists. Please check your configuration on the Specter dashboard");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to deserialize meta data {key} with {e.GetType().Name}: {e.Message}");
            }

            return success;
        }
    }
}