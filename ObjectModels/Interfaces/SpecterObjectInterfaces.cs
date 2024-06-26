using System;
using System.Collections.Generic;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.ObjectModels.Interfaces
{
    public interface ISpecterObject { }

    public interface ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public bool TryGetMeta<T>(string key, out T val)
        {
            bool success = false;
            val = default;
            
            try
            {
                if (Meta.TryGetValue(key, out object objVal))
                {
                    if (SpecterJson.TryConvertObject<T>(objVal, out val))
                        success = true;
                }
                else
                    SPDebug.LogWarning($"No meta data with key {key} exists. Please check your configuration on the Specter dashboard");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to deserialize meta data {key} with {e.GetType().Name}: {e.Message}");
            }

            return success;
        }
    }
}