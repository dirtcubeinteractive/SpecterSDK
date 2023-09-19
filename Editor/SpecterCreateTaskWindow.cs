using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using SpecterSDK.Shared;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace SpecterSDK.Editor
{
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
            public RewardType Type { get; set; }
            public int? IntId { get; set; }
            public string Id { get; set; }
            public int Quantity { get; set; }
        }
        
        [Serializable]
        private class TaskCreateModel
        {
            public string Name { get; set; }
            public string TaskId { get; set; }
            public TaskType Type { get; set; }
            public string EventId { get; set; }
            public string Description { get; set; }
            public string IconUrl { get; set; }
            public RewardClaim RewardClaim { get; set; }
            public bool IsLockedByLevel { get; set; }
            public SpecterQueryBuilder.Group BusinessLogics { get; set; }
            public List<object> Config { get; set; }
        }
        
        private TaskCreateModel m_Task;
        private List<RewardCreateModel> m_RewardCreateModels;
        
        public SpecterQueryBuilder.Group Query => m_QueryBuilder?.Root;
        private SpecterQueryBuilder m_QueryBuilder;

        private float m_SectionSpacing = 10f;
        private Vector2 m_ScrollPosition;

        public static void ShowWindow()
        {
            var window = GetWindow<SpecterCreateTaskWindow>("Create Specter Task", true);
            window.minSize = new Vector2(640f, 360f);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            m_Task = new TaskCreateModel();
            m_RewardCreateModels = new List<RewardCreateModel>();
            m_QueryBuilder = new SpecterQueryBuilder(scrollable: false);
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
            if (GUILayout.Button("Create", GUILayout.Height(40f)))
            {
                Debug.Log(JsonConvert.SerializeObject(m_Task));
            }
        }

        private void DrawTaskConfig()
        {
            int oriFontSize = GUI.skin.font.fontSize;
            
            EditorGUILayout.LabelField("TASK CONFIG", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("Task Name");
                        m_Task.Name = EditorGUILayout.TextField(m_Task.Name);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("Task ID");
                        m_Task.TaskId = EditorGUILayout.TextField(m_Task.TaskId);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("Event ID");
                        m_Task.EventId = EditorGUILayout.TextField(m_Task.EventId);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawRewardConfigs()
        {
            EditorGUILayout.LabelField("REWARD CONFIG", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            {
                if (GUILayout.Button("Add Reward"))
                {
                    m_RewardCreateModels.Add(new RewardCreateModel());
                }
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
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Reward Type");
                    EditorGUI.BeginChangeCheck();
                    rewardConfig.Type = (RewardType)EditorGUILayout.EnumPopup(rewardConfig.Type);
                    if (EditorGUI.EndChangeCheck())
                    {
                        rewardConfig.Id = null;
                        rewardConfig.IntId = null;
                    }
                }
                EditorGUILayout.EndVertical();
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
