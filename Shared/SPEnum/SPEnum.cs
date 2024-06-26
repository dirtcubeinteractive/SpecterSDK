using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SpecterSDK.Shared.SPEnum
{
    /// <summary>
    /// A strongly-typed enum with additional properties and methods.
    /// </summary>
    [Serializable]
    public abstract class SPEnum<TEnum> : IComparable
    where TEnum : SPEnum<TEnum>
    {
        [SerializeField] private string m_Name;
        public string Name => m_Name;

        [SerializeField] private int m_Id;
        public int Id => m_Id;

        /// <summary>
        /// Retrieves the display name for the current enum value.
        /// </summary>
        /// <returns>The display name associated with the enum value.</returns>
        [SerializeField] private string m_DisplayName;
        public string DisplayName
        {
            get => m_DisplayName ?? Name;
            set => m_DisplayName = value;
        }
        
#if UNITY_EDITOR
        public const string NAME_PROP_NAME = nameof(m_Name);
        public const string ID_PROP_NAME = nameof(m_Id);
        public const string DISPLAYNAME_PROP_NAME = nameof(m_DisplayName);
#endif

        protected SPEnum(int id, string name, string displayName = null) => (m_Id, m_Name, m_DisplayName) = (id, name, displayName);

        public override string ToString() => Name;
        
        public static IEnumerable<T> GetValues<T>() where T : SPEnum<TEnum> =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object obj) =>
            (obj is SPEnum<TEnum> other) && Equals(other);

        protected virtual bool Equals(SPEnum<TEnum> other)
        {
            // check if same instance
            if (ReferenceEquals(this, other))
                return true;

            // it's not same instance so 
            // check if it's not null and is same value
            return other is not null && Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(SPEnum<TEnum> firstValue, SPEnum<TEnum> secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static TEnum FromValue(int value)
        {
            var matchingItem = Parse<TEnum, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static TEnum FromDisplayName(string displayName)
        {
            var matchingItem = Parse<TEnum, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : SPEnum<TEnum>
        {
            var matchingItem = GetValues<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((SPEnum<TEnum>)other).Id);
        
        public static bool operator ==(SPEnum<TEnum> lhs, SPEnum<TEnum> rhs)
        {
            if (lhs is null)
                return rhs is null;
            
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(SPEnum<TEnum> lhs, SPEnum<TEnum> rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator int(SPEnum<TEnum> spEnum)
        {
            return spEnum?.Id ?? default;
        }

        public static explicit operator SPEnum<TEnum>(int id)
        {
            return FromValue(id);
        }
    }
}