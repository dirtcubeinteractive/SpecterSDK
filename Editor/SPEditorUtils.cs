using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor.Utils
{
    public static class SPEditorUtils
    {
        public static void SelectOrCreateScriptableObject<T>(string fileNameWithoutExtension, string resourcesParent, params string[] subDirs) where T : ScriptableObject
        {
            string resolvedParentDirPath = !string.IsNullOrEmpty(resourcesParent) ? $"{resourcesParent}/" : "";
            
            string resolvedSubDirPath = "";
            foreach (var subDir in subDirs)
            {
                resolvedSubDirPath += $"{subDir}/";
            }

            string loadPath = $"{resolvedSubDirPath}{fileNameWithoutExtension}";
            string path = $"Assets/{resolvedParentDirPath}Resources/{loadPath}.asset";
            T asset = Resources.Load<T>(loadPath);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                
                if (!string.IsNullOrEmpty(resourcesParent) && !Directory.Exists($"Assets/{resourcesParent}"))
                    AssetDatabase.CreateFolder("Assets", resourcesParent);
                
                if (!Directory.Exists($"Assets/{resolvedParentDirPath}Resources"))
                {
                    AssetDatabase.CreateFolder($"Assets/{resourcesParent}", "Resources");
                }

                if (subDirs.Length > 0)
                {
                    var dirPath = $"Assets/{resolvedParentDirPath}Resources";
                    foreach (var subDir in subDirs)
                    {
                        if (!Directory.Exists($"{dirPath}/{subDir}"))
                            AssetDatabase.CreateFolder(dirPath, subDir);
                        dirPath += $"/{subDir}";
                    }
                }
                
                CreateAsset(asset, path);
            }

            Selection.activeObject = asset;
        }

        public static void CreateAsset<T>(T asset, string path) where T: Object
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
