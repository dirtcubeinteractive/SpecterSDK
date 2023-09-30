using System;
using System.Linq;
using System.Collections.Generic;
using SpecterSDK.Editor.API;
using SpecterSDK.Shared;
using SpecterSDK.Shared.EventSystem;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor
{
    using Shared.SPEnum;
    
    public abstract class SpecterEditorWindow : EditorWindow
    {
        protected static string ConfigProjectContextPropName => SpecterConfigData.ProjectContextProp_Id;
        
        protected SPEditorApiClient ApiClient;

        protected virtual void OnEnable()
        {
            ReloadClient();
        }

        protected virtual void OnFocus()
        {
            SpecterSdkEventHandler.UnregisterEvent(SpecterConfigData.PropertyEventKey(ConfigProjectContextPropName), SPSharedEvents.Editor.k_OnVitalConfigPropChanged, OnProjectIdentifiersChanged);
            ReloadClient();
        }

        protected virtual void OnLostFocus()
        {
            SpecterSdkEventHandler.RegisterEvent(SpecterConfigData.PropertyEventKey(ConfigProjectContextPropName), SPSharedEvents.Editor.k_OnVitalConfigPropChanged, OnProjectIdentifiersChanged);
        }

        private void ReloadClient()
        {
            if (ApiClient == null)
            {
                Debug.Log($"{GetType().Name}: " + (ApiClient == null ? "Initializing Editor Api Client" : "Config changed...Reloading Editor Api Client."));
                ApiClient = new SPEditorApiClient(Specter.LoadConfig());
            }
        }

        protected virtual void OnProjectIdentifiersChanged()
        {
            Debug.Log(SPSharedEvents.Editor.k_OnVitalConfigPropChanged);
            ApiClient = null;
        }

        protected virtual void DrawHelpBox(string message, MessageType messageType)
        {
            EditorGUILayout.HelpBox(message, messageType);
        }

        protected virtual void DrawHelpNeutral(string message)
        {
            DrawHelpBox(message, MessageType.None);
        }

        protected virtual void DrawInfo(string message)
        {
            DrawHelpBox(message, MessageType.Info);
        }

        protected virtual void DrawWarning(string message)
        {
            DrawHelpBox(message, MessageType.Warning);
        }

        protected virtual void DrawError(string message)
        {
            DrawHelpBox(message, MessageType.Error);
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

        protected virtual void DrawTextAreaVertical(string label, ref string val, Action onTextChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField("Task Description");
                DrawTextArea(ref val, onTextChanged, style, options);
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

        protected virtual void DrawEnumPopupVertical<T>(string label, ref T val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options) where T : Enum
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawEnumPopup(ref val, onValueChanged, style, options);
            }
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawPopupVertical(string label, ref int selectedIndex, string[] selectionTitles, Action onValueChanged = null)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawPopup(ref selectedIndex, selectionTitles, onValueChanged);
            }
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawSPEnumPopupVertical<T>(string label, ref T val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options) where T : SPEnum<T>
        {
            EditorGUILayout.BeginVertical();
            {
                DrawLabelField(label);
                DrawSPEnumPopup(ref val, onValueChanged, style, options);
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

        protected virtual void DrawTextArea(ref string val, Action onTextChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            style ??= EditorStyles.textArea;

            if (options == null || options.Length == 0)
                options = new GUILayoutOption[] { GUILayout.Height(60f) };
            
            EditorGUI.BeginChangeCheck();
            val = EditorGUILayout.TextArea(val, style, options);
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
        
        protected virtual void DrawIntField(ref int? val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            val ??= default;
            style ??= EditorStyles.textField;

            EditorGUI.BeginChangeCheck();
            val = EditorGUILayout.IntField(val.Value, style, options);
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

        protected virtual void DrawPopup(ref int selectedIndex, string[] selectionTitles, Action onSelectionChanged = null, GUIStyle style = null, params GUILayoutOption[] options)
        {
            style ??= EditorStyles.popup;
            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUILayout.Popup(selectedIndex, selectionTitles, style, options);
            if (EditorGUI.EndChangeCheck())
                onSelectionChanged?.Invoke();
        }

        protected virtual void DrawSPEnumPopup<T>(ref T val, Action onValueChanged = null, GUIStyle style = null, params GUILayoutOption[] options) where T : SPEnum<T>
        {
            style ??= EditorStyles.popup;
            var enumValues = SPEnum<T>.GetValues<T>().ToList();
            var index = enumValues.IndexOf(val);
            EditorGUI.BeginChangeCheck();
            {
                index = EditorGUILayout.Popup(index, GetValueNames(enumValues), style, options);
                val = enumValues[index];
            }
            if (EditorGUI.EndChangeCheck())
                onValueChanged?.Invoke();

            GUIContent[] GetValueNames(List<T> values)
            {
                var names = new GUIContent[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    names[i] = new GUIContent(values[i].DisplayName);
                }
                return names;
            }
        }
    }
}