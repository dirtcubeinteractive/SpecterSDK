namespace SpecterSDK.Shared.EventSystem
{
    /// <summary>
    /// Internal SDK events. DO NOT CONFUSE WITH Specter Api Events
    /// </summary>
    public static class SPSharedEvents
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