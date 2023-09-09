using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpecterSDK.Editor.Utils;

namespace SpecterSDK.Editor
{
    public static class SpecterMenuItems
    {
        [MenuItem("Specter/Select Specter Config")]
        public static void SelectOrCreateSpecterConfigData()
        {
            SPEditorUtils.SelectOrCreateScriptableObject<SpecterConfigData>("SpecterConfigData", "SpecterSDK", "Config");
        }
    }
}
