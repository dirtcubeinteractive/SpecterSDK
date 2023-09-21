using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor
{
    public class SpecterTasksWindow : SpecterEditorWindow, ICreateTaskWindowDelegate
    {
        private List<SPTaskAdminModel> m_Tasks;
        private List<SPAppEvent> m_AppEvents;

        private readonly string[] m_TabTitles = new string[] { "Tasks", "Events" };
        private int m_SelectedTabIndex;
        
        private Vector2 m_TaskListScrollPos;
        private Vector2 m_EventListScrollPos;
        
        [MenuItem("Window/Specter/Tasks")]
        public static void ShowWindow()
        {
            var window = GetWindow<SpecterTasksWindow>("Specter Tasks", true);
            window.minSize = new Vector2(720f, 360f);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            FetchTasks();
        }

        private async void FetchTasks()
        {
            m_Tasks = await ApiClient.GetTaskList(new SPGetTaskListAdminRequest());
            Repaint();
        }
        
        private async void FetchEvents(Action onFetchedEvents = null)
        {
            m_AppEvents = new List<SPAppEvent>();

            var defaultEventData = await ApiClient.GetDefaultEvents(new SPGetDefaultEventsAdminRequest());
            var customEventData = await ApiClient.GetCustomEvents(new SPGetCustomEventsAdminRequest());
            
            if (customEventData != null)
                m_AppEvents.AddRange(customEventData.appEventDetails);
            if (defaultEventData != null)
                m_AppEvents.AddRange(defaultEventData.appEventDetails);
            
            Repaint();
            if (m_AppEvents != null)
                onFetchedEvents?.Invoke();
        }

        public List<SPAppEvent> GetAppEvents()
        {
            return m_AppEvents;
        }

        private void OnGUI()
        {
            DrawTabs();
        }
        
        private void DrawTabs()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginChangeCheck();
            m_SelectedTabIndex = GUILayout.SelectionGrid(m_SelectedTabIndex, m_TabTitles, m_TabTitles.Length, GUILayout.Width(320));
            if (EditorGUI.EndChangeCheck())
            {
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            switch (m_SelectedTabIndex)
            {
                case 0:
                    DrawTasksTab();
                    break;
                case 1:
                    DrawEventsTab();
                    break;
            }
        }

        private void DrawTasksTab()
        {
            DrawTaskTableHeaders();
            DrawTasksTable();
        }

        private void DrawTaskTableHeaders()
        {
            DrawTableHeaders(new[] {"ID", "Name", "Rewards" });
        }

        private void DrawTasksTable()
        {
            EditorGUILayout.BeginVertical();
            if (m_Tasks == null || m_Tasks.Count == 0)
            {
                DrawEmptyState("No tasks created in this Specter project");
            }
            else
            {
                m_TaskListScrollPos = EditorGUILayout.BeginScrollView(m_TaskListScrollPos);
                {
                    EditorGUILayout.BeginVertical();
                    {
                        foreach (var task in m_Tasks)
                        {
                            DrawTaskRow(task);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndScrollView();
            }

            DrawButtons(new string[] { "Create New Task", "Refresh"}, ShowCreateTaskWindow, FetchTasks);
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
        }

        private void DrawButtons(string[] buttonTitles = null, Action createAction = null, Action refreshAction = null)
        {
            if (buttonTitles is not { Length: 2 })
                buttonTitles = new string[] { "Create", "Refresh" };

            EditorGUILayout.BeginHorizontal();
            {
                var buttonHeight = 40f;
                DrawButton(() =>
                {
                    Debug.Log("Creating...");
                    createAction?.Invoke();
                }, buttonTitles[0], null, GUILayout.Height(buttonHeight));

                DrawButton(() =>
                {
                    Debug.Log("Refreshing...");
                    refreshAction?.Invoke();
                }, buttonTitles[1], null, GUILayout.Width(60f), GUILayout.Height(buttonHeight));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawEmptyState(string message)
        {
            EditorGUILayout.LabelField(message);
        }

        private void DrawTaskRow(SPTaskAdminModel task)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.SelectableLabel(task.taskId, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel(task.name, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel($"{task.taskRewards.Count}", EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ShowCreateTaskWindow()
        {
            if (m_AppEvents == null || m_AppEvents.Count == 0)
                FetchEvents(() => SpecterCreateTaskWindow.ShowWindow(m_AppEvents));
            else
                SpecterCreateTaskWindow.ShowWindow(m_AppEvents);
        }

        private void DrawEventsTab()
        {
            DrawEventTableHeaders();
            DrawEventsTable();
        }
        
        private void DrawEventTableHeaders()
        {
            string[] headers = { "Event Name", "ID", "Parameters" };
            EditorGUILayout.BeginHorizontal("box");
            {
                foreach (var header in headers)
                {
                    EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
                }
                //GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawEventsTable()
        {
            EditorGUILayout.BeginVertical();
            if (m_AppEvents == null || m_AppEvents.Count == 0)
            {
                DrawEmptyState("No custom events created for this Specter project");
            }
            else
            {
                m_EventListScrollPos = EditorGUILayout.BeginScrollView(m_EventListScrollPos);
                {
                    EditorGUILayout.BeginVertical();
                    {
                        foreach (var appEvent in m_AppEvents)
                        {
                            DrawEventRow(appEvent);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndScrollView();
            }

            DrawButtons(new string[] { "Create New Event", "Refresh"}, null, () => FetchEvents());
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
        }

        private void DrawEventRow(SPAppEvent appEvent)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.SelectableLabel(appEvent.name, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel(appEvent.id, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel($"{appEvent.GetAllParameters().Count}", EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                //GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
