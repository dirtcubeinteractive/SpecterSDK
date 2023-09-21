using System;
using UnityEngine;
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

        protected virtual void DrawTableHeaders(string[] headers, GUIStyle style = null)
        {
            style ??= "box";
            EditorGUILayout.BeginHorizontal(style);
            {
                foreach (var header in headers)
                {
                    EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
                }
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawButton(Action onButtonClicked, string buttonLabel = "Button", GUIStyle style = null,  params GUILayoutOption[] options)
        {
            style ??= GUI.skin.button;
            
            if (GUILayout.Button(buttonLabel, style, options))
            {
                onButtonClicked.Invoke();
            }
        }

        protected virtual void DrawTextFieldVertical(string label, ref string val, Action onTextChanged = null)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawTextField(ref val, onTextChanged);
            }
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawIntFieldVertical(string label, ref int val, Action onValueChanged = null)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawIntField(ref val, onValueChanged);
            }
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawEnumPopupVertical<T>(string label, ref T val, Action onValueChanged = null) where T : Enum
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawEnumPopup(ref val, onValueChanged);
            }
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawLabelField(string label, GUIStyle style = null, params GUILayoutOption[] options)
        {
            style ??= EditorStyles.label;
            EditorGUILayout.LabelField(label, style, options);
        }

        protected virtual void DrawTextField(ref string val, Action onTextChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            style ??= EditorStyles.textField;
            EditorGUI.BeginChangeCheck();
            val = EditorGUILayout.TextField(val, style, options);
            if (EditorGUI.EndChangeCheck())
                onTextChanged?.Invoke();
        }

        protected virtual void DrawIntField(ref int val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            style ??= EditorStyles.textField;
            EditorGUI.BeginChangeCheck();
            val = EditorGUILayout.IntField(val, style, options);
            if (EditorGUI.EndChangeCheck())
                onValueChanged?.Invoke();
        }

        protected virtual void DrawEnumPopup<T>(ref T val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options) where T : Enum
        {
            style ??= EditorStyles.popup;
            EditorGUI.BeginChangeCheck();
            val = (T)EditorGUILayout.EnumPopup(val, style, options);
            if (EditorGUI.EndChangeCheck())
                onValueChanged?.Invoke();
        }
    }
}