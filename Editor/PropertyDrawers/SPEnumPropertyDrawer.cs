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
            var nameProp = property.FindPropertyRelative(SPEnum<TEnum>.NAME_PROP_NAME);
            var idProp = property.FindPropertyRelative(SPEnum<TEnum>.ID_PROP_NAME);
            var displayNameProp = property.FindPropertyRelative(SPEnum<TEnum>.DISPLAYNAME_PROP_NAME);
            
            EditorGUI.BeginProperty(position, label, property);
            var values = SPEnum<TEnum>.GetValues<TEnum>().ToList();
            var index = values.IndexOf(values.FirstOrDefault(v => v.Id == idProp.intValue));
            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, label, index, GetValueNames(values));
            if (EditorGUI.EndChangeCheck())
            {
                var value = values[index];
                idProp.intValue = value.Id;
                nameProp.stringValue = value.Name;
                displayNameProp.stringValue = value.DisplayName;
            }
            
            EditorGUI.EndProperty();
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