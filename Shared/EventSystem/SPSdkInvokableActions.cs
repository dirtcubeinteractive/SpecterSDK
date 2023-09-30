using System;
using UnityEngine.Scripting;

namespace SpecterSDK.Shared.EventSystem
{
    [Preserve]
    internal abstract class SPSdkInvokableActionBase
    {

    }

    internal class SPSdkInvokableAction : SPSdkInvokableActionBase
    {
        private event Action m_Action;

        public void Initialize(Action action)
        {
            this.m_Action = action;
        }

        public void Invoke()
        {
            this.m_Action();
        }

        public bool IsAction(Action action)
        {
            return this.m_Action == action;
        }
    }

    [Preserve]
    internal class SPSdkInvokableAction<T1> : SPSdkInvokableActionBase
    {
        private event Action<T1> m_Action;

        public void Initialize(Action<T1> action)
        {
            this.m_Action = action;
        }

        public void Invoke(T1 arg1)
        {
            this.m_Action(arg1);
        }

        public bool IsAction(Action<T1> action)
        {
            return this.m_Action == action;
        }
    }

    [Preserve]
    internal class SPSdkInvokableAction<T1, T2> : SPSdkInvokableActionBase
    {
        private event Action<T1, T2> m_Action;

        public void Initialize(Action<T1, T2> action)
        {
            this.m_Action = action;
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            this.m_Action(arg1, arg2);
        }

        public bool IsAction(Action<T1, T2> action)
        {
            return this.m_Action == action;
        }
    }

    [Preserve]
    internal class SPSdkInvokableAction<T1, T2, T3> : SPSdkInvokableActionBase
    {
        private event Action<T1, T2, T3> m_Action;

        public void Initialize(Action<T1, T2, T3> action)
        {
            this.m_Action = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            this.m_Action(arg1, arg2, arg3);
        }

        public bool IsAction(Action<T1, T2, T3> action)
        {
            return this.m_Action == action;
        }
    }
}