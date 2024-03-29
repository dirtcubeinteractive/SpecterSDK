using System.Collections.Generic;
using System.Linq;
using SpecterSDK.Editor.Utils;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;
using UnityEditor;
using UnityEngine;

namespace SpecterSDK.Editor
{
    public abstract class SPEnumPropertyDrawer<TEnum> : PropertyDrawer
        where TEnum : SPEnum<TEnum>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var values = SPEnum<TEnum>.GetValues<TEnum>().ToList();
            EditorGUI.BeginChangeCheck();
            {
                var index = values.ToList().IndexOf(property.GetUnderlyingValue() as TEnum);
                index = EditorGUI.Popup(position, label, index, GetValueNames(values));
                property.SetUnderlyingValue(values[index]);
            }
            EditorGUI.EndChangeCheck();
        }

        private static GUIContent[] GetValueNames(IReadOnlyList<TEnum> values)
        {
            var names = new GUIContent[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                names[i] = new GUIContent(values[i].DisplayName);
            }
            return names;
        }
    }
    
    [CustomPropertyDrawer(typeof(SPRewardSourceType))]
    public class SPRewardSourceTypePropertyDrawer : SPEnumPropertyDrawer<SPRewardSourceType> { }

    [CustomPropertyDrawer(typeof(SPRewardGrantType))]
    public class SPRewardGrantTypePropertyDrawer : SPEnumPropertyDrawer<SPRewardGrantType> { }
    
    [CustomPropertyDrawer(typeof(SPTaskType))]
    public class SPTaskTypePropertyDrawer : SPEnumPropertyDrawer<SPTaskType> { }
    
    [CustomPropertyDrawer(typeof(SPTaskStatus))]
    public class SPTaskStatusPropertyDrawer : SPEnumPropertyDrawer<SPTaskStatus> { }
}