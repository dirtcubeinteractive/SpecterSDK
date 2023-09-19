using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpecterSDK;
using SpecterSDK.Editor.Utils;
using SpecterSDK.Shared;

namespace SpecterSDK.Editor
{
    public static class SpecterMenuItems
    {
        [MenuItem("Specter/Select Specter Config")]
        public static void SelectOrCreateSpecterConfigData()
        {
            Selection.activeObject = SPEditorUtils.LoadOrCreateScriptableObjectResource<SpecterConfigData>(Specter.CONFIG_FILENAME,  parentDirectoryPath: Specter.SDK_DIRNAME, subDirectoryPath: Specter.SHARED_DATA_DIRNAME);
        }
    }
}
