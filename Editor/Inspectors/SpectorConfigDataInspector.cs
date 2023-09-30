using SpecterSDK.Shared;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor.Inspectors
{
    [CustomEditor(typeof(SpecterConfigData))]
    public class SpectorConfigDataInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DoDrawDefaultInspector();
        }

        internal bool DoDrawDefaultInspector()
        {
            bool flag;
            using (new LocalizationGroup((object)this.target))
            {
                flag = DoDrawDefaultInspector(this.serializedObject);
                MonoBehaviour targetMono = this.target as MonoBehaviour;
                if ((UnityEngine.Object)targetMono == (UnityEngine.Object)null)
                    return flag;
            }

            return flag;
        }

        internal static bool DoDrawDefaultInspector(SerializedObject obj)
        {
            EditorGUI.BeginChangeCheck();
            obj.UpdateIfRequiredOrScript();
            SerializedProperty iterator = obj.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                {
                    if (iterator.name.Equals(SpecterConfigData.DebugAuthContextProp_Id))
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            EditorGUILayout.PropertyField(iterator, true);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            Debug.Log("Change detected");
                        }
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(iterator, true);
                    }
                }
            }

            obj.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }
    }
}