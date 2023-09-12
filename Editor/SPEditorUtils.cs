using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace SpecterSDK.Editor.Utils
{
    public static class SPEditorUtils
    {
        /// <summary>
        /// Utility function to load an instance of a ScriptableObject resource in the project
        /// window. Will create an instance if one does not exist. To be used for objects that
        /// need to be accessed from Resources.
        /// </summary>
        /// <param name="fileNameWithoutExtension">File name of the SO without .asset extension</param>
        /// <param name="parentDirectoryPath">Part of the path before 'Resources' (Do not include leading and trailing '/', or 'Assets' folder in the path)</param>
        /// <param name="subDirectoryPath">Part of the path after 'Resources' (Do not include leading '/' or file name</param>
        /// <typeparam name="T">Subclass of the ScriptableObject to be selected</typeparam>
        public static T LoadOrCreateScriptableObjectResource<T>(string fileNameWithoutExtension, string parentDirectoryPath = "", string subDirectoryPath = "") where T : ScriptableObject
        {
            string resourcesPath = $"{parentDirectoryPath}/Resources/{subDirectoryPath}".Trim('/');
            string fullPath = $"Assets/{resourcesPath}/{fileNameWithoutExtension}.asset";
    
            // Check if the asset already exists at the specified path
            T asset = AssetDatabase.LoadAssetAtPath<T>(fullPath);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                if (!Directory.Exists($"Assets/{resourcesPath}"))
                {
                    CreateSubDirectories(resourcesPath);
                }
        
                CreateAsset(asset, fullPath);
            }

            return asset;
        }

        /// <summary>
        /// Helper function to create a full subdirectory path
        /// </summary>
        /// <param name="directoryPath">Subdirectories to create within a root/parent dir</param>
        /// <param name="rootDir">Root/main parent dir within which to create subdirectories. Default root is "Assets"</param>
        private static void CreateSubDirectories(string directoryPath, string rootDir = "Assets")
        {
            string[] subDirs = directoryPath.Split('/');
            string currentDir = rootDir;
            foreach (string subDir in subDirs)
            {
                string nextDir = $"{currentDir}/{subDir}";
                if (!AssetDatabase.IsValidFolder(nextDir))
                {
                    AssetDatabase.CreateFolder(currentDir, subDir);
                }
                currentDir = nextDir;
            }
        }

        /// <summary>
        /// Helper function to create an asset
        /// </summary>
        /// <param name="asset">Asset to create</param>
        /// <param name="path">Path to asset</param>
        /// <typeparam name="T"></typeparam>
        public static void CreateAsset<T>(T asset, string path) where T: Object
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
