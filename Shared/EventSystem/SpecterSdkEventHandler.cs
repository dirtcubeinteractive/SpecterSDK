using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Utilities;
using UnityEngine;

namespace SpecterSDK.Shared.EventSystem
{
    // ReSharper disable PossibleNullReferenceException
    
    public class SpecterSdkEventHandler : MonoBehaviour
    {
        private static readonly Dictionary<object, Dictionary<string, List<SPSdkInvokableActionBase>>> s_EventTable = new();
        private static readonly Dictionary<string, List<SPSdkInvokableActionBase>> s_GlobalEventTable = new();

        private static void RegisterEvent(string eventName, SPSdkInvokableActionBase invokableAction)
        {
            if (s_GlobalEventTable.TryGetValue(eventName, out var value))
            {
                value.Add(invokableAction);
                return;
            }

            value = new List<SPSdkInvokableActionBase> { invokableAction };
            s_GlobalEventTable.Add(eventName, value);
        }

        private static void RegisterEvent(object obj, string eventName, SPSdkInvokableActionBase invokableAction)
        {
            if (!s_EventTable.TryGetValue(obj, out var value))
            {
                value = new Dictionary<string, List<SPSdkInvokableActionBase>>();
                s_EventTable.Add(obj, value);
            }

            if (value.TryGetValue(eventName, out var value2))
            {
                value2.Add(invokableAction);
                return;
            }

            value2 = new List<SPSdkInvokableActionBase> { invokableAction };
            value.Add(eventName, value2);
        }

        private static List<SPSdkInvokableActionBase> GetActionList(string eventName)
        {
            if (s_GlobalEventTable.TryGetValue(eventName, out var value))
            {
                return value;
            }

            return null;
        }

        private static void CheckForEventRemoval(string eventName, List<SPSdkInvokableActionBase> actionList)
        {
            if (actionList.Count == 0)
            {
                s_GlobalEventTable.Remove(eventName);
            }
        }

        private static List<SPSdkInvokableActionBase> GetActionList(object obj, string eventName)
        {
            if (s_EventTable.TryGetValue(obj, out var value) && value.TryGetValue(eventName, out var value2))
            {
                return value2;
            }

            return null;
        }

        private static void CheckForEventRemoval(object obj, string eventName, List<SPSdkInvokableActionBase> actionList)
        {
            if (actionList.Count == 0 && s_EventTable.TryGetValue(obj, out var value))
            {
                value.Remove(eventName);
                if (value.Count == 0)
                {
                    s_EventTable.Remove(obj);
                }
            }
        }

        public static void RegisterEvent(string eventName, Action action)
        {
            SPSdkInvokableAction invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction>();
            invokableAction.Initialize(action);
            RegisterEvent(eventName, invokableAction);
        }

        public static void RegisterEvent(object obj, string eventName, Action action)
        {
            SPSdkInvokableAction invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction>();
            invokableAction.Initialize(action);
            RegisterEvent(obj, eventName, invokableAction);
        }

        public static void RegisterEvent<T1>(string eventName, Action<T1> action)
        {
            SPSdkInvokableAction<T1> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1>>();
            invokableAction.Initialize(action);
            RegisterEvent(eventName, invokableAction);
        }

        public static void RegisterEvent<T1>(object obj, string eventName, Action<T1> action)
        {
            SPSdkInvokableAction<T1> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1>>();
            invokableAction.Initialize(action);
            RegisterEvent(obj, eventName, invokableAction);
        }

        public static void RegisterEvent<T1, T2>(string eventName, Action<T1, T2> action)
        {
            SPSdkInvokableAction<T1, T2> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1, T2>>();
            invokableAction.Initialize(action);
            RegisterEvent(eventName, invokableAction);
        }

        public static void RegisterEvent<T1, T2>(object obj, string eventName, Action<T1, T2> action)
        {
            SPSdkInvokableAction<T1, T2> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1, T2>>();
            invokableAction.Initialize(action);
            RegisterEvent(obj, eventName, invokableAction);
        }

        public static void RegisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
        {
            SPSdkInvokableAction<T1, T2, T3> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1, T2, T3>>();
            invokableAction.Initialize(action);
            RegisterEvent(eventName, invokableAction);
        }

        public static void RegisterEvent<T1, T2, T3>(object obj, string eventName, Action<T1, T2, T3> action)
        {
            SPSdkInvokableAction<T1, T2, T3> invokableAction = SPGenericObjectPool.Get<SPSdkInvokableAction<T1, T2, T3>>();
            invokableAction.Initialize(action);
            RegisterEvent(obj, eventName, invokableAction);
        }

        public static void ExecuteEvent(string eventName)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction).Invoke();
                }
            }
        }

        public static void ExecuteEvent(object obj, string eventName)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction).Invoke();
                }
            }
        }

        public static void ExecuteEvent<T1>(string eventName, T1 arg1)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1>).Invoke(arg1);
                }
            }
        }

        public static void ExecuteEvent<T1>(object obj, string eventName, T1 arg1)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1>).Invoke(arg1);
                }
            }
        }

        public static void ExecuteEvent<T1, T2>(string eventName, T1 arg1, T2 arg2)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1, T2>).Invoke(arg1, arg2);
                }
            }
        }

        public static void ExecuteEvent<T1, T2>(object obj, string eventName, T1 arg1, T2 arg2)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1, T2>).Invoke(arg1, arg2);
                }
            }
        }

        public static void ExecuteEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1, T2, T3>).Invoke(arg1, arg2, arg3);
                }
            }
        }

        public static void ExecuteEvent<T1, T2, T3>(object obj, string eventName, T1 arg1, T2 arg2, T3 arg3)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList != null)
            {
                for (int num = actionList.Count - 1; num >= 0; num--)
                {
                    (actionList[num] as SPSdkInvokableAction<T1, T2, T3>).Invoke(arg1, arg2, arg3);
                }
            }
        }

        public static void UnregisterEvent(string eventName, Action action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction invokableAction = actionList[i] as SPSdkInvokableAction;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(eventName, actionList);
        }

        public static void UnregisterEvent(object obj, string eventName, Action action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction invokableAction = actionList[i] as SPSdkInvokableAction;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(obj, eventName, actionList);
        }

        public static void UnregisterEvent<T1>(string eventName, Action<T1> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1> invokableAction = actionList[i] as SPSdkInvokableAction<T1>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(eventName, actionList);
        }

        public static void UnregisterEvent<T1>(object obj, string eventName, Action<T1> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1> invokableAction = actionList[i] as SPSdkInvokableAction<T1>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(obj, eventName, actionList);
        }

        public static void UnregisterEvent<T1, T2>(string eventName, Action<T1, T2> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1, T2> invokableAction = actionList[i] as SPSdkInvokableAction<T1, T2>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1, T2>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(eventName, actionList);
        }

        public static void UnregisterEvent<T1, T2>(object obj, string eventName, Action<T1, T2> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1, T2> invokableAction = actionList[i] as SPSdkInvokableAction<T1, T2>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1, T2>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(obj, eventName, actionList);
        }

        public static void UnregisterEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1, T2, T3> invokableAction = actionList[i] as SPSdkInvokableAction<T1, T2, T3>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1, T2, T3>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(eventName, actionList);
        }

        public static void UnregisterEvent<T1, T2, T3>(object obj, string eventName, Action<T1, T2, T3> action)
        {
            List<SPSdkInvokableActionBase> actionList = GetActionList(obj, eventName);
            if (actionList == null)
            {
                return;
            }

            for (int i = 0; i < actionList.Count; i++)
            {
                SPSdkInvokableAction<T1, T2, T3> invokableAction = actionList[i] as SPSdkInvokableAction<T1, T2, T3>;
                if (invokableAction.IsAction(action))
                {
                    SPGenericObjectPool.Return<SPSdkInvokableAction<T1, T2, T3>>(invokableAction);
                    actionList.RemoveAt(i);
                    break;
                }
            }

            CheckForEventRemoval(obj, eventName, actionList);
        }

        private void OnDisable()
        {
            if (!((UnityEngine.Object)(object)((Component)this).gameObject != (UnityEngine.Object)null) ||
                ((Component)this).gameObject.activeSelf)
            {
                ClearTable();
            }
        }

        private void OnDestroy()
        {
            ClearTable();
        }

        private void ClearTable()
        {
            s_EventTable.Clear();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReset()
        {
            s_EventTable?.Clear();
            s_GlobalEventTable?.Clear();
        }
    }
}