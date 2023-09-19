using System;
using UnityEditor;

namespace SpecterSDK.Editor
{
    public abstract class SpecterEditorWindow : EditorWindow
    {
        protected static SPEditorApiClient ApiClient;

        protected virtual void OnEnable()
        {
            ApiClient ??= new SPEditorApiClient(Specter.LoadConfig());
        }
    }
}