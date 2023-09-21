using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;
using SpecterSDK.Shared;
using Unity.Plastic.Newtonsoft.Json;
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
        private enum RewardClaim
        {
            OnClaim,
            Automatic
        }
        
        private enum TaskType
        {
            Static,
            Dynamic
        }
        
        private enum RewardType
        {
            ProgressionMarker,
            Currency,
            Item,
            Bundle
        }

        [Serializable]
        private class RewardCreateModel
        {
            public RewardType Type;
            public int? IntId { get; set; }
            public string Id { get; set; }
            public int Quantity { get; set; }
        }
        
        [Serializable]
        private class TaskCreateModel
        {
            public string Name;
            public string TaskId;
            public TaskType Type;
            public string EventId;
            public string Description;
            public string IconUrl;
            public RewardClaim RewardClaim;
            public bool IsLockedByLevel;
            public Dictionary<string, object> BusinessLogics;
            public List<Dictionary<string, object>> Config;
        }

        private SPCreateTaskAdminRequest m_CreateTask;
        private TaskCreateModel m_Task;
        private List<RewardCreateModel> m_RewardCreateModels;
        private List<SPAppEvent> m_AppEvents;

        private int m_SelectedEventIndex;
        
        public SpecterQueryBuilder.Group Query => m_QueryBuilder?.Root;
        private SpecterQueryBuilder m_QueryBuilder;

        private float m_SectionSpacing = 10f;
        private Vector2 m_ScrollPosition;

        public static void ShowWindow(List<SPAppEvent> appEvents)
        {
            var window = GetWindow<SpecterCreateTaskWindow>("Create Specter Task", true);
            window.minSize = new Vector2(640f, 360f);
            window.m_AppEvents = appEvents;
            
            window.m_Task = new TaskCreateModel();
            window.m_CreateTask = new SPCreateTaskAdminRequest();
            window.m_RewardCreateModels = new List<RewardCreateModel>();
            window.m_QueryBuilder = new SpecterQueryBuilder(scrollable: false);
            window.m_QueryBuilder.SetParameters(appEvents[0].GetAllParameters());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
        }

        private void OnGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            {
                EditorGUILayout.BeginVertical();
                {
                    DrawTaskConfig();
                    GUILayout.Space(m_SectionSpacing);
                    DrawRewardConfigs();
                    GUILayout.Space(m_SectionSpacing);
                    DrawParamsConfig();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
            DrawButton(CreateTask, "Create", null, GUILayout.Height(40f));
        }

        private void CreateTask()
        {
            m_Task.BusinessLogics = m_QueryBuilder.BuildBusinessLogics();
            m_Task.Config = m_QueryBuilder.GetConfigs();
            Debug.Log(JsonConvert.SerializeObject(m_Task, Formatting.Indented));
        }

        private void DrawTaskConfig()
        {
            int oriFontSize = GUI.skin.font.fontSize;
            
            EditorGUILayout.LabelField("TASK CONFIG", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    DrawTextFieldVertical("Task Name", ref m_Task.Name);
                    DrawTextFieldVertical("Task ID", ref m_Task.TaskId);
                    EditorGUILayout.BeginVertical();
                    {
                        DrawLabelField("Event ID");
                        EditorGUI.BeginChangeCheck();
                        m_SelectedEventIndex = EditorGUILayout.Popup(m_SelectedEventIndex, GetEventNames());
                        if (EditorGUI.EndChangeCheck())
                        {
                            m_Task.EventId = m_AppEvents[m_SelectedEventIndex].id;
                            m_QueryBuilder.SetParameters(m_AppEvents[m_SelectedEventIndex].GetAllParameters());
                            Repaint();
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private string[] GetEventNames()
        {
            if (m_AppEvents == null || m_AppEvents.Count == 0)
                return new[] { "None" };

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
                DrawButton(() => m_RewardCreateModels.Add(new RewardCreateModel()), "Add Reward");
                for (int i = 0; i < m_RewardCreateModels.Count; i++)
                {
                    DrawRewardConfigRow(m_RewardCreateModels[i]);
                }
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawRewardConfigRow(RewardCreateModel rewardConfig)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawEnumPopupVertical("Reward Type", ref rewardConfig.Type);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Resource Id");
                    switch (rewardConfig.Type)
                    {
                        case RewardType.ProgressionMarker:
                        case RewardType.Currency:
                            rewardConfig.IntId ??= -1;
                            rewardConfig.IntId = EditorGUILayout.IntField(rewardConfig.IntId.Value);
                            break;
                        case RewardType.Item:
                        case RewardType.Bundle:
                            rewardConfig.Id = EditorGUILayout.TextField(rewardConfig.Id);
                            break;
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Quantity");
                    rewardConfig.Quantity = EditorGUILayout.IntField(rewardConfig.Quantity);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight * 2));
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("X", EditorStyles.largeLabel))
                            m_RewardCreateModels.Remove(rewardConfig);
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
