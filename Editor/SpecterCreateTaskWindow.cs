using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace SpecterSDK.Editor
{
    public interface ICreateTaskWindowDelegate
    {
        public List<SPAppEvent> GetAppEvents();
    }
    
    public class SpecterCreateTaskWindow : SpecterEditorWindow
    {
        private const float SECTION_SPACING = 10f;
        
        private SPCreateTaskAdminRequest m_CreateTask;
        private List<SPTaskRewardConfig> m_RewardConfigs;
        private List<SPAppEvent> m_AppEvents;
        private SpecterQueryBuilder m_QueryBuilder;
        
        private Vector2 m_ScrollPosition;
        private int m_SelectedEventIndex;
        private bool m_CreatingTask;

        public static void ShowWindow(List<SPAppEvent> appEvents)
        {
            var window = GetWindow<SpecterCreateTaskWindow>("Create Specter Task", true);
            window.minSize = new Vector2(640f, 360f);
            window.m_AppEvents = appEvents;
            window.ResetTaskData();
        }

        private void OnGUI()
        {
            bool disableUI = m_AppEvents == null || m_AppEvents.Count == 0;
            if (disableUI)
                EditorGUILayout.HelpBox("This Specter project has no events created for tasks. Please configure at least one event to create tasks", MessageType.Error);
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUI.BeginDisabledGroup(disableUI);
                    {
                        DrawTaskConfig();
                        GUILayout.Space(SECTION_SPACING);
                        DrawRewardConfigs();
                        GUILayout.Space(SECTION_SPACING);
                        DrawParamsConfig();
                    }
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
            EditorGUI.BeginDisabledGroup(m_CreatingTask || disableUI);
            {
                DrawButton(CreateTask, "Create", null, GUILayout.Height(40f));
            }
            EditorGUI.EndDisabledGroup();
        }

        private async void CreateTask()
        {
            m_CreatingTask = true;
            var selectedEvent = m_AppEvents[m_SelectedEventIndex];
            
            m_CreateTask.businessLogic = m_QueryBuilder.BuildBusinessLogics();
            m_CreateTask.config = m_QueryBuilder.GetConfigs();
            m_CreateTask.defaultEventId = selectedEvent.type == nameof(SPAppEventType.Default).ToLower() ? m_CreateTask.eventId : null;
            m_CreateTask.customEventId = selectedEvent.type == nameof(SPAppEventType.Custom).ToLower() ? m_CreateTask.eventId : null;

            try
            {
                Debug.Log(JsonConvert.SerializeObject(m_CreateTask, Formatting.Indented));
                var result = await ApiClient.CreateTask(m_CreateTask);
                if (result != null)
                {
                    ResetTaskData();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            m_CreatingTask = false;
            Repaint();
        }

        private void ResetTaskData()
        {
            m_SelectedEventIndex = 0;
            m_CreateTask = new SPCreateTaskAdminRequest();
            m_RewardConfigs = m_CreateTask.rewardDetails;
            ResetTaskEventData();
        }

        private void ResetTaskEventData()
        {
            m_QueryBuilder = new SpecterQueryBuilder(scrollable: false);
            if (m_AppEvents is not { Count: > 0 }) 
                return;
            
            m_CreateTask.eventId = m_AppEvents[m_SelectedEventIndex].id;
            m_QueryBuilder.SetParameters(m_AppEvents?[m_SelectedEventIndex]?.GetAllParameters() ?? new List<SPAppEventParameter>());
        }

        private void DrawTaskConfig()
        {
            EditorGUILayout.LabelField("TASK CONFIG", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    DrawTextFieldVertical("Task Name", ref m_CreateTask.name);
                    DrawTextFieldVertical("Task ID", ref m_CreateTask.taskId);
                    DrawPopupVertical("Event ID", ref m_SelectedEventIndex, GetEventNames(), ResetTaskEventData);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(8f);
                DrawTextAreaVertical("Task Description", 
                    ref m_CreateTask.description, 
                    null, 
                    new GUIStyle(GUI.skin.textArea) { margin = new RectOffset(5, 5, GUI.skin.textArea.margin.top, GUI.skin.textArea.margin.top) },
                    GUILayout.Height(60f));
            }
            EditorGUILayout.EndVertical();
        }

        private string[] GetEventNames()
        {
            if (m_AppEvents == null || m_AppEvents.Count == 0)
                return Array.Empty<string>();

            var eventNames = new List<string>();
            foreach (var appEvent in m_AppEvents)
            {
                eventNames.Add(appEvent.name);
            }

            return eventNames.ToArray();
        }

        private void DrawRewardConfigs()
        {
            EditorGUILayout.LabelField("REWARD CONFIG", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                DrawButton(() => m_RewardConfigs.Add(new SPTaskRewardConfig()), "Add Reward");
                for (int i = 0; i < m_RewardConfigs.Count; i++)
                {
                    DrawRewardConfigRow(m_RewardConfigs[i]);
                }
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawRewardConfigRow(SPTaskRewardConfig rewardConfig)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawEnumPopupVertical("Reward Type", ref rewardConfig.type, () =>
                {
                    rewardConfig.progressionMarkerId = null;
                    rewardConfig.currencyId = null;
                    rewardConfig.itemId = null;
                    rewardConfig.bundleId = null;
                });
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Resource Id");
                    switch (rewardConfig.type)
                    {
                        case SPRewardType.ProgressionMarker:
                            rewardConfig.progressionMarkerId ??= -1;
                            DrawIntField(ref rewardConfig.progressionMarkerId);
                            break;
                        case SPRewardType.Currency:
                            rewardConfig.currencyId ??= -1;
                            DrawIntField(ref rewardConfig.currencyId);
                            break;
                        case SPRewardType.Item:
                            DrawTextField(ref rewardConfig.itemId);
                            break;
                        case SPRewardType.Bundle:
                            DrawTextField(ref rewardConfig.bundleId);
                            break;
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Quantity");
                    rewardConfig.quantity = EditorGUILayout.IntField(rewardConfig.quantity);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight * 2));
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("X", EditorStyles.largeLabel))
                            m_RewardConfigs.Remove(rewardConfig);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawParamsConfig()
        {
            EditorGUILayout.LabelField("PARAMETERS CONFIG", EditorStyles.boldLabel);
            m_QueryBuilder.DrawGUI();
        }
    }
}
