using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;

namespace SpecterSDK.Editor.Utils
{
    public static class SPSerializedPropertyUtility
    {
        static SPSerializedPropertyUtility()
        {

        }


        #region Helpers

        public static string FixedPropertyPath(this SerializedProperty property)
        {
            // Unity structures array paths like "fieldName.Array.data[i]".
            // Fix that quirk and directly go to index, i.e. "fieldName[i]".
            return property.propertyPath.Replace(".Array.data[", "[");
        }

        public static string[] PropertyPathParts(this SerializedProperty property)
        {
            return property.FixedPropertyPath().Split('.');
        }

        public static bool IsPropertyIndexer(string propertyPart, out string fieldName, out int index)
        {
            var regex = new Regex(@"(.+)\[(\d+)\]");
            var match = regex.Match(propertyPart);

            if (match.Success) // Property refers to an array or list
            {
                fieldName = match.Groups[1].Value;
                index = int.Parse(match.Groups[2].Value);
                return true;
            }
            else
            {
                fieldName = propertyPart;
                index = -1;
                return false;
            }
        }

        #endregion

        #region Reflection

        private static void EnsureReflectable(SerializedProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.serializedObject.isEditingMultipleObjects)
            {
                throw new NotSupportedException(
                    $"Attempting to reflect property '{property.propertyPath}' on multiple objects.");
            }

            if (property.serializedObject.targetObject == null)
            {
                throw new NotSupportedException(
                    $"Attempting to reflect property '{property.propertyPath}' on a null object.");
            }
        }

        private static string SerializedObjectLabel(SerializedObject serializedObject)
        {
            return serializedObject.isEditingMultipleObjects
                ? "[Multiple]"
                : serializedObject.targetObject.GetType().Name;
        }

        public static object GetUnderlyingValue(this SerializedProperty property)
        {
            EnsureReflectable(property);

            object parent = property.serializedObject.targetObject;
            var parts = PropertyPathParts(property);

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];

                if (parent == null)
                {
                    throw new NullReferenceException(
                        $"Parent of '{SerializedObjectLabel(property.serializedObject)}.{string.Join(".", parts, 0, i + 1)}' is null.");
                }

                parent = GetPropertyPartValue(part, parent);
            }

            return parent;
        }

        public static void SetUnderlyingValue(this SerializedProperty property, object value)
        {
            EnsureReflectable(property);

            // Serialize so we don't overwrite other modifications with our deserialization later
            property.serializedObject.ApplyModifiedPropertiesWithoutUndo();

            object parent = property.serializedObject.targetObject;
            var parts = PropertyPathParts(property);

            for (var i = 0; i < parts.Length - 1; i++)
            {
                var part = parts[i];

                if (parent == null)
                {
                    throw new NullReferenceException(
                        $"Parent of '{SerializedObjectLabel(property.serializedObject)}.{string.Join(".", parts, 0, i + 1)}' is null.");
                }

                parent = GetPropertyPartValue(part, parent);
            }

            string fieldName;
            int index;
            IsPropertyIndexer(parts[parts.Length - 1], out fieldName, out index);

            var field = GetSerializedFieldInfo(parent.GetType(), fieldName);

            field.SetValue(parent, value);

            // Deserialize the object for continued operations after this call
            property.serializedObject.Update();
        }

        private static object GetPropertyPartValue(string propertyPathPart, object parent)
        {
            string fieldName;
            int index;

            if (IsPropertyIndexer(propertyPathPart, out fieldName, out index))
            {
                var list = (IList)GetSerializedFieldInfo(parent.GetType(), fieldName).GetValue(parent);

                return list[index];
            }
            else
            {
                return GetSerializedFieldInfo(parent.GetType(), fieldName).GetValue(parent);
            }
        }

        private static FieldInfo GetSerializedFieldInfo(Type type, string name)
        {
            var field = type.GetFieldUnambiguous(name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (field == null)
            {
                throw new MissingMemberException(type.FullName, name);
            }

            return field;
        }

        public static FieldInfo GetFieldUnambiguous(this Type type, string name, BindingFlags flags)
        {
            if (type == null)
                throw new ArgumentNullException("Type cannot be null when getting a field");
            if (name == null)
                throw new ArgumentNullException("Field name cannot be null");

            flags |= BindingFlags.DeclaredOnly;

            while (type != null)
            {
                var field = type.GetField(name, flags);

                if (field != null)
                {
                    return field;
                }

                type = type.BaseType;
            }

            return null;
        }

        #endregion
    }
}