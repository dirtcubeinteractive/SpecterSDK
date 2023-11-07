using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels.AdminModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Editor.API;
using SpecterSDK.Shared;
using UnityEditor;
using UnityEngine;

namespace SpecterSDK.Editor.DashboardTools
{
    public class SpecterCreateTaskWindow : SpecterEditorWindow
    {
        private enum State
        {
            Ready,
            Initializing,
            CreatingTask
        }

        private const string DEBUG_MODE_WARN = "You are not in Debug Mode. If you click 'Create' your task creation event will be executed";
        private const string NO_EVENTS_ERROR = "This Specter project has no events created for tasks. Please configure at least one event to create tasks";
        private const float SECTION_SPACING = 10f;
        
        private SPCreateTaskAdminRequest m_CreateTask;
        
        private List<SPTaskRewardConfig> m_RewardConfigs;
        private List<SPProgressionAccessControlConfig> m_ProgressionConfigs;
        private List<SPMetaConfig> m_MetaConfigs;
        private string m_TagsString;

        private List<SPAppEvent> m_AppEvents;
        private List<SPProgressionSystemAdminData> m_ProgressionSystems;
        
        private SpecterQueryBuilder m_QueryBuilder;
        
        private Vector2 m_ScrollPosition;
        private int m_SelectedEventIndex;
        private bool m_IsDebug;
        private State m_State;

        public static void ShowWindow()
        {
            var window = GetWindow<SpecterCreateTaskWindow>("Create Specter Task", true);
            window.minSize = new Vector2(720f, 360f);
            window.Initialize();
        }

        private async void Initialize()
        {
            m_State = State.Initializing;
            
            await FetchEvents();
            await FetchProgressionSystems();
            
            ResetTaskData();
            m_State = State.Ready;
            Repaint();
        }

        private async Task FetchEvents()
        {
            var allEvents = await ApiClient.GetEvents();
            m_AppEvents = allEvents;
        }
        
        private async Task FetchProgressionSystems()
        {
            var progressionSystemsResult = await ApiClient.GetProgressionSystems(new SPGetProgressionSystemsAdminRequest());
            m_ProgressionSystems = progressionSystemsResult.LevelDetails;
        }

        private void OnGUI()
        {
            if (m_State == State.Initializing)
            {
                DrawHelpNeutral("Loading data...");
                return;
            }
            
            bool disableUI = m_AppEvents == null || m_AppEvents.Count == 0;
            if (disableUI)
                DrawError(NO_EVENTS_ERROR);
            
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        m_IsDebug = EditorGUILayout.ToggleLeft("Debug Mode", m_IsDebug, GUILayout.Width(100f));
                        DrawButton(Initialize, "Refresh Data", null, GUILayout.Width(100f));
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.BeginDisabledGroup(disableUI);
                    {
                        DrawTaskConfig();
                        GUILayout.Space(SECTION_SPACING);
                        DrawCustomDataConfig();
                        DrawAccessControlConfig();
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
            
            if (!m_IsDebug)
                DrawWarning(DEBUG_MODE_WARN);
            
            EditorGUI.BeginDisabledGroup(m_State != State.Ready || disableUI);
            {
                DrawButton(CreateTask, "Create", null, GUILayout.Height(40f));
            }
            EditorGUI.EndDisabledGroup();
        }

        private async void CreateTask()
        {
            m_State = State.CreatingTask;
            var selectedEvent = m_AppEvents[m_SelectedEventIndex];
            
            m_CreateTask.businessLogic = m_QueryBuilder.BuildBusinessLogics();
            m_CreateTask.config = m_QueryBuilder.GetConfigs();
            m_CreateTask.defaultEventId = selectedEvent.type == nameof(SPAppEventType.Default).ToLower() ? m_CreateTask.eventId : null;
            m_CreateTask.customEventId = selectedEvent.type == nameof(SPAppEventType.Custom).ToLower() ? m_CreateTask.eventId : null;

            if (!m_CreateTask.isLockedByLevel)
                m_CreateTask.levelDetails.Clear();
            else
                m_CreateTask.levelDetails = m_ProgressionConfigs;

            m_CreateTask.rewardDetails = m_RewardConfigs;
            BuildTags();
            
            try
            {
                BuildMetaData();
                
                Debug.Log(SpecterJson.SerializeObject(m_CreateTask, SPJsonFormatting.Indented));
                if (!m_IsDebug)
                    await SendCreateRequest();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            m_State = State.Ready;
            Repaint();
        }

        private void BuildTags()
        {
            m_CreateTask.tags.Clear();
            var tagsString = m_TagsString?.Trim();
            if (string.IsNullOrEmpty(tagsString))
                return;
            
            var tags = tagsString.Split(',');
            foreach (var tag in tags)
            {
                var addTag = tag.Trim();
                if (!m_CreateTask.tags.Contains(addTag) && !string.IsNullOrEmpty(addTag))
                    m_CreateTask.tags.Add(addTag);
            }
        }

        private void BuildMetaData()
        {
            m_CreateTask.meta.Clear();
            foreach (var metaConfig in m_MetaConfigs)
            {
                if (string.IsNullOrEmpty(metaConfig.key) || string.IsNullOrEmpty(metaConfig.value))
                    continue;
                m_CreateTask.meta.Add(metaConfig.key, metaConfig.value);
            }
        }

        private async Task SendCreateRequest()
        {
            var result = await ApiClient.CreateTask(m_CreateTask);
            if (result != null)
            {
                ResetTaskData();
            }
        }

        private void ResetTaskData()
        {
            m_SelectedEventIndex = 0;
            m_TagsString = "";
            m_CreateTask = new SPCreateTaskAdminRequest();
            m_RewardConfigs = new List<SPTaskRewardConfig>();
            m_ProgressionConfigs = new List<SPProgressionAccessControlConfig>();
            m_MetaConfigs = new List<SPMetaConfig>();
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
                EditorGUILayout.Space(8f);
                EditorGUILayout.BeginHorizontal();
                {
                    DrawSPEnumPopupVertical("Task Type", ref m_CreateTask.type);
                    EditorGUILayout.Space(5f);
                    DrawSPEnumPopupVertical("Reward Grant", ref m_CreateTask.rewardGrantType);
                    EditorGUILayout.Space(5f);
                    EditorGUILayout.BeginVertical();
                    {
                        m_CreateTask.isLockedByLevel = EditorGUILayout.ToggleLeft("Is Locked By Level", m_CreateTask.isLockedByLevel, GUILayout.MaxWidth(160f));
                        m_CreateTask.isRecurring = EditorGUILayout.ToggleLeft("Is Recurring", m_CreateTask.isRecurring, GUILayout.MaxWidth(160f));
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawCustomDataConfig()
        {
            DrawLabelField("CUSTOM DATA");
            EditorGUILayout.BeginVertical("box");
            {
                DrawTextFieldVertical("Tags", ref m_TagsString);
                DrawLabelField("Meta Data");
                DrawButton(AddMetaField, "Add Meta");
                for (int i = 0; i < m_MetaConfigs.Count; i++)
                {
                    DrawMetaRow(m_MetaConfigs[i]);
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void AddMetaField()
        {
            var meta = new SPMetaConfig();
            m_MetaConfigs.Add(meta);
        }

        private void DrawMetaRow(SPMetaConfig metaConfig)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawTextFieldVertical("Key", ref metaConfig.key, () =>
                {
                    metaConfig.key = metaConfig.key.Trim();
                });
                DrawTextFieldVertical("Value", ref metaConfig.value, () =>
                {
                    metaConfig.value = metaConfig.value.Trim();
                });
                EditorGUILayout.BeginVertical(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight * 2));
                {
                    GUILayout.FlexibleSpace();
                    DrawButton(
                        () => m_MetaConfigs.Remove(metaConfig), 
                        "X", EditorStyles.largeLabel);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAccessControlConfig()
        {
            if (!m_CreateTask.isLockedByLevel)
                return;

            if (m_ProgressionSystems == null || m_ProgressionSystems.Count == 0)
            {
                return;
            }
            
            GUILayout.Space(SECTION_SPACING);
            DrawLabelField("ACCESS & ELIGIBILITY", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                DrawButton(AddProgressionConfig, "Add Progression System");
                for (int i = 0; i < m_ProgressionConfigs.Count; i++)
                {
                    DrawProgressionConfigRow(m_ProgressionConfigs[i]);
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void AddProgressionConfig()
        {
            var config = new SPProgressionAccessControlConfig();
            config.levelSystemId = m_ProgressionSystems[config.selectedSystemIndex].id;
            config.level = m_ProgressionSystems[config.selectedSystemIndex].levelSystemLevelMapping?[0]?.levelNo ?? 0;
            m_ProgressionConfigs.Add(config);
        }

        private void DrawProgressionConfigRow(SPProgressionAccessControlConfig progressionConfig)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawPopupVertical("Progression System", 
                    ref progressionConfig.selectedSystemIndex, 
                    selectionTitles: GetProgressionSystemNames(), 
                    onValueChanged: () =>
                    {
                        progressionConfig.levelSystemId = m_ProgressionSystems[progressionConfig.selectedSystemIndex].id;
                    });
                DrawIntFieldVertical("Level", ref progressionConfig.level);
                EditorGUILayout.BeginVertical(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight * 2));
                {
                    GUILayout.FlexibleSpace();
                    DrawButton(
                        () => m_ProgressionConfigs.Remove(progressionConfig), 
                        "X", EditorStyles.largeLabel);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private string[] GetProgressionSystemNames()
        {
            var names = new List<string>();
            foreach (SPProgressionSystemAdminData progressionSystem in m_ProgressionSystems)
            {
                names.Add(progressionSystem.name);
            }

            return names.ToArray();
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
