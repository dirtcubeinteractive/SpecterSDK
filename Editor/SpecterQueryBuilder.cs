using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace SpecterSDK.Editor
{
    [Serializable]
    public class SpecterQueryBuilder
    {
        public enum Combinator
        {
            AND,
            OR
        }

        public enum Operator
        {
            equal,
            notEqual,
            greaterThan,
            lessThan,
            greaterThanOrEqual,
            lessThanOrEqual
        }

        public enum ParamType
        {
            oneshot,
            cumulative
        }

        [Serializable]
        public class Rule
        {
            public string parameterName;
            public string op = nameof(Operator.equal);
            public string value;

            public string type = nameof(ParamType.oneshot);
            public bool allTime = true;
            public int numRecords = 0;
        }

        [Serializable]
        public class Group
        {
            public string combinator = nameof(Combinator.AND).ToLower();
            public List<object> children = new(); // This can contain either Rule or Group
        }

        public Group Root { get; } = new Group();

        #region GUI variables

        public bool useScroll;
        private Vector2 scrollPosition = Vector2.zero;

        #endregion

        public SpecterQueryBuilder(bool scrollable = false)
        {
            useScroll = scrollable;
        }
        
        public void DrawGUI()
        {
            if (useScroll)
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            DrawGroup(Root);
            if (useScroll)
                GUILayout.EndScrollView();
        }
        
        private void DrawGroup(Group group, Group parentGroup = null)
        {
            if (parentGroup != null)
                EditorGUI.indentLevel++;

            EditorGUILayout.BeginVertical("box");
            {
                DrawControlButtons(group, parentGroup);
            
                for (int i = 0; i < group.children.Count; i++)
                {
                    if (group.children[i] is Rule)
                    {
                        DrawRule((Rule)group.children[i], group);
                    }
                    else if (group.children[i] is Group)
                    {
                        var defaultBgColor = GUI.backgroundColor;
                        GUI.backgroundColor = Color.grey;
                        DrawGroup((Group)group.children[i], group);
                        GUI.backgroundColor = defaultBgColor;
                    }
                }
            }

            EditorGUILayout.EndVertical();

            if (parentGroup != null)
                EditorGUI.indentLevel--;
        }

        private void DrawControlButtons(Group group, Group parentGroup)
        {
            bool isRoot = parentGroup == null;
            EditorGUILayout.BeginHorizontal();
            {
                group.combinator = ((Combinator)EditorGUILayout.EnumPopup(System.Enum.Parse<Combinator>(group.combinator.ToUpper()), GUILayout.Width(100))).ToString().ToLower();
                if (!isRoot)
                {
                    if(GUILayout.Button("Add Rule", GUILayout.Width(100)))
                    {
                        group.children.Add(new Rule());
                    }
                    if(GUILayout.Button("Delete Group", GUILayout.Width(100)))
                    {
                        parentGroup.children.Remove(group);
                    }
                }
                else
                {
                    if(GUILayout.Button("Add Group", GUILayout.Width(100)))
                    {
                        group.children.Add(new Group());
                    }
                    
                    if(GUILayout.Button("Generate JSON", GUILayout.Width(160)))
                    {
                        string jsonString = GenerateJSON(Root);
                        Debug.Log(jsonString);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawRule(Rule rule, Group parentGroup)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawParamFields(rule, parentGroup);
                DrawParamConfig(rule, parentGroup);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawParamFields(Rule rule, Group parentGroup)
        {
            EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.ExpandHeight(false) });
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Param Name");
                    rule.parameterName = EditorGUILayout.TextField(rule.parameterName, EditorStyles.textField,
                        new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Operator");
                    rule.op = ((Operator)EditorGUILayout.EnumPopup(Enum.Parse<Operator>(rule.op))).ToString();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Value");
                    rule.value = EditorGUILayout.TextField(rule.value);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                {
                    GUILayout.Space(20);
                    if (GUILayout.Button("Delete", GUILayout.Width(60)))
                    {
                        parentGroup.children.Remove(rule);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawParamConfig(Rule rule, Group parentGroup)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(false), GUILayout.Width(100));
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Param Type");
                    EditorGUILayout.BeginHorizontal();
                    {
                        bool oneshot = EditorGUILayout.ToggleLeft("One Shot", rule.type == nameof(ParamType.oneshot), GUILayout.Width(100));
                        if (oneshot)
                            rule.type = nameof(ParamType.oneshot);

                        bool cumulative = EditorGUILayout.ToggleLeft("Cumulative", rule.type == nameof(ParamType.cumulative), GUILayout.Width(100));
                        if (cumulative)
                            rule.type = nameof(ParamType.cumulative);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                if (rule.type == nameof(ParamType.cumulative))
                {
                    GUILayout.Space(20);
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.LabelField("All Time");
                        EditorGUILayout.BeginHorizontal();
                        {
                            bool allTime = EditorGUILayout.ToggleLeft("Yes", rule.allTime, GUILayout.Width(100));
                            if (allTime)
                                rule.allTime = true;

                            bool notAllTime =
                                EditorGUILayout.ToggleLeft("No", rule.allTime == false, GUILayout.Width(100));
                            if (notAllTime)
                                rule.allTime = false;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                    if (rule.allTime != true)
                    {
                        GUILayout.Space(5);
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.LabelField("Num Records");
                            rule.numRecords = EditorGUILayout.IntField(rule.numRecords);
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private string GenerateJSON(Group root)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(root, Formatting.Indented);
        }
    }

    public class QueryBuilderWindow : EditorWindow
    {
        public SpecterQueryBuilder.Group Query => queryBuilder?.Root;
        private readonly SpecterQueryBuilder queryBuilder = new();

        private SerializedObject serializedObject;

        [MenuItem("Window/Specter/Query Builder")]
        public static void ShowWindow()
        {
            GetWindow<QueryBuilderWindow>("Query Builder");
        }

        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
        }

        private void OnGUI()
        {
            serializedObject.Update();
            queryBuilder.DrawGUI();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDestroy()
        {
            serializedObject = null;
        }
    }
}