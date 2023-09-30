namespace SpecterSDK.Shared.EventSystem
{
    public class SPSharedEvents
    {
#if UNITY_EDITOR
        public struct Editor
        {
            // When a vital property in the config Scriptable Object (eg: project id)
            public const string k_OnVitalConfigPropChanged = nameof(k_OnVitalConfigPropChanged);
        }
#endif
    }
}