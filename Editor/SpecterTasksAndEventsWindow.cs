using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor
{
    public class SpecterTasksAndEventsWindow : SpecterEditorWindow
    {
        private List<SPTaskAdminModel> m_Tasks;
        private List<SPAppEvent> m_AppEvents;

        private readonly string[] m_TabTitles = { "Tasks", "Events" };
        private int m_SelectedTabIndex;
        
        private Vector2 m_TaskListScrollPos;
        private Vector2 m_EventListScrollPos;
        
        [MenuItem("Specter/Tasks & Events")]
        public static void ShowWindow()
        {
            var window = GetWindow<SpecterTasksAndEventsWindow>("Specter Tasks & Events", true);
            window.minSize = new Vector2(720f, 360f);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Initialize();
        }

        private async void Initialize()
        {
            await FetchTasks();
            await FetchEvents();
        }

        private async Task FetchTasks()
        {
            m_Tasks = await ApiClient.GetTaskList(new SPGetTaskListAdminRequest());
            Repaint();
        }
        
        private async Task FetchEvents()
        {
            m_AppEvents = await ApiClient.GetEvents();
            Repaint();
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

            DrawButtons(new [] { "Create New Task", "Refresh"}, ShowCreateTaskWindow, () =>
            {
                FetchTasks().GetAwaiter().OnCompleted(() => Debug.Log("Tasks refreshed!"));
            });
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
        }

        private void DrawButtons(string[] buttonTitles = null, Action createAction = null, Action refreshAction = null)
        {
            if (buttonTitles is not { Length: 2 })
                buttonTitles = new[] { "Create", "Refresh" };

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
            DrawLabelField(message);
        }

        private void DrawTaskRow(SPTaskAdminModel task)
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.SelectableLabel(task.taskId, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel(task.name, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.SelectableLabel($"{task.currencies.Count + task.bundles.Count + task.items.Count + task.progressionMarkers.Count}", EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                DrawButton(() => Debug.Log(JsonConvert.SerializeObject(task, Formatting.Indented)), "View");
                // DrawButton(() => { }, "Edit");
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ShowCreateTaskWindow()
        {
            SpecterCreateTaskWindow.ShowWindow();
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
                    DrawLabelField(header, EditorStyles.boldLabel);
                }
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

            DrawButtons(new [] { "Create New Event", "Refresh"}, null, () =>
            {
                FetchEvents().GetAwaiter().OnCompleted(() => Debug.Log("Events refreshed!"));
            });
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
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
