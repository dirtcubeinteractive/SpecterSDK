using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

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
            public string parameterId;
            public string parameterName;
            public string @operator = nameof(Operator.equal);
            public object value;

            public string incrementalType = nameof(ParamType.oneshot);
            public bool allTime = true;
            public int numberOfRecords = 0;
            public DateTime? startTime = null;
            public DateTime? endTime = null;
            
            [Newtonsoft.Json.JsonIgnore]
            public int selectedParameterIndex { get; set; }
            [Newtonsoft.Json.JsonIgnore]
            public SPParamDataType dataType { get; set; }
        }

        [Serializable]
        public class SPParamConfig
        {
            public string parameterId;
            public string parameterName;
            public string incrementalType = nameof(ParamType.oneshot);
            public int? numberOfRecords;
            public DateTime? startTime;
            public DateTime? endTime;

            [JsonIgnore]
            public bool allTime = true;
            [JsonIgnore] 
            public int selectedParameterIndex;
        }

        [Serializable]
        public class Group
        {
            public string combinator = nameof(Combinator.AND).ToLower();
            public List<object> children = new(); // This can contain either Rule or Group
        }

        public Group Root { get; private set; } = new Group();
        private List<SPAppEventParameter> m_AppEventParameters;

        public HashSet<string> ConfigParamIds = new();
        public List<Dictionary<string, object>> Configs = new ();

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
            if (m_AppEventParameters == null || m_AppEventParameters.Count == 0)
            {
                EditorGUILayout.HelpBox("This event has no parameters", MessageType.Info);
                return;
            }
            
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
                group.combinator = ((Combinator)EditorGUILayout.EnumPopup(Enum.Parse<Combinator>(group.combinator.ToUpper()), GUILayout.Width(100))).ToString().ToLower();
                
                if(GUILayout.Button("Add Rule", GUILayout.Width(100)))
                {
                    var rule = new Rule();
                    group.children.Add(rule);
                }
                if (!isRoot)
                {
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
                        string jsonString = GenerateJSON();
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
                DrawParamConfig(rule);
            }
            EditorGUILayout.EndVertical();
        }

        public void SetParameters(List<SPAppEventParameter> eventParams)
        {
            m_AppEventParameters = eventParams;
        }
        
        private string[] GetParameterNames()
        {
            var paramNames = new List<string>();
            foreach (var appEventParam in m_AppEventParameters)
            {
                paramNames.Add(appEventParam.name);
            }

            return paramNames.ToArray();
        }

        private void DrawParamFields(Rule rule, Group parentGroup)
        {
            EditorGUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.ExpandHeight(false) });
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Param Name");
                    EditorGUI.BeginChangeCheck();
                    rule.selectedParameterIndex = EditorGUILayout.Popup(rule.selectedParameterIndex, GetParameterNames());
                    if (rule.parameterId == null || EditorGUI.EndChangeCheck())
                    {
                        rule.parameterId = m_AppEventParameters[rule.selectedParameterIndex].id;
                        rule.parameterName = m_AppEventParameters[rule.selectedParameterIndex].name;
                        rule.dataType = m_AppEventParameters[rule.selectedParameterIndex].dataTypeId;
                        rule.value = default;
                    }
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Operator");
                    rule.@operator = ((Operator)EditorGUILayout.EnumPopup(Enum.Parse<Operator>(rule.@operator))).ToString();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Value");
                    switch (rule.dataType)
                    {
                        case SPParamDataType.String:
                            rule.value = EditorGUILayout.TextField((string)rule.value);
                            break;
                        case SPParamDataType.Integer:
                            rule.value ??= 0;
                            rule.value = EditorGUILayout.IntField((int)rule.value);
                            break;
                        case SPParamDataType.Boolean:
                            rule.value ??= false;
                            rule.value = EditorGUILayout.Toggle((bool)rule.value);
                            break;
                        case SPParamDataType.Float:
                            rule.value ??= 0f;
                            rule.value = EditorGUILayout.FloatField((float)rule.value);
                            break;
                        default:
                            throw new NotImplementedException($"Work In Progress -- data type {rule.dataType} not implemented");
                    }
                    
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

        private void DrawParamConfig(Rule rule)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(false), GUILayout.Width(100));
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Param Type");
                    EditorGUILayout.BeginHorizontal();
                    {
                        bool oneshot = EditorGUILayout.ToggleLeft("One Shot", rule.incrementalType == nameof(ParamType.oneshot), GUILayout.Width(100));
                        if (oneshot)
                            rule.incrementalType = nameof(ParamType.oneshot);

                        bool cumulative = EditorGUILayout.ToggleLeft("Cumulative", rule.incrementalType == nameof(ParamType.cumulative), GUILayout.Width(100));
                        if (cumulative)
                            rule.incrementalType = nameof(ParamType.cumulative);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                if (rule.incrementalType == nameof(ParamType.cumulative))
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
                            rule.numberOfRecords = EditorGUILayout.IntField(rule.numberOfRecords);
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private string GenerateJSON()
        {
            var queryDict = new Dictionary<string, object>()
            {
                { "businessLogics", BuildBusinessLogics() },
                { "config", GetConfigs() }
            };
            return JsonConvert.SerializeObject(queryDict, Formatting.Indented);
        }
        
        public Dictionary<string, object> BuildBusinessLogics()
        {
            return TransformGroupToDictionary(Root, true);
        }

        public List<Dictionary<string, object>> GetConfigs()
        {
            return Configs;
        }
        
        private Dictionary<string, object> TransformGroupToDictionary(Group group, bool isRoot)
        {
            if (isRoot)
            {
                ConfigParamIds.Clear();
                Configs.Clear();
            }
            
            Dictionary<string, object> result = new Dictionary<string, object>();

            List<object> conditionsList = new List<object>();
            foreach (var child in group.children)
            {
                if (child is Group childGroup)
                {
                    conditionsList.Add(TransformGroupToDictionary(childGroup, false));
                }
                else if (child is Rule childRule)
                {
                    var rule = TransformRuleToDictionary(childRule);
                    if (rule != null)
                        conditionsList.Add(rule);
                }
            }

            string conditionKey = group.combinator == "and" ? "all" : "any";
            result[conditionKey] = conditionsList;

            return result;
        }

        private Dictionary<string, object> TransformRuleToDictionary(Rule rule)
        {
            if (ConfigParamIds.Contains(rule.parameterName))
                return null;

            ConfigParamIds.Add(rule.parameterName);
            Configs.Add(new Dictionary<string, object>()
            {
                { nameof(rule.parameterId), rule.parameterId },
                { nameof(rule.parameterName), rule.parameterName },
                { nameof(rule.@operator), rule.@operator == "equal" ? "equalTo" : ConvertOperator(rule.@operator) },
                { nameof(rule.value), rule.value },
                { nameof(rule.incrementalType), rule.incrementalType == "oneshot" ? "one-shot" : rule.incrementalType },
                { nameof(rule.numberOfRecords), rule.numberOfRecords },
                { nameof(rule.startTime), rule.startTime },
                { nameof(rule.endTime), rule.endTime }
            });
            
            return new Dictionary<string, object>
            {
                { "fact", rule.parameterName },
                { nameof(rule.@operator), ConvertOperator(rule.@operator) },
                { nameof(rule.value), rule.value }
            };
        }

        private string ConvertOperator(string op)
        {
            return op switch
            {
                nameof(Operator.greaterThanOrEqual) => "greaterThanInclusive",
                nameof(Operator.lessThanOrEqual) => "lessThanInclusive",
                _ => op
            };
        }
    }
}