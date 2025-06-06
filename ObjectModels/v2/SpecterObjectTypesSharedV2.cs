using System;

namespace SpecterSDK.ObjectModels.v2
{
    [Flags]
    public enum SPAccessLockType
    {
        None = 0,
        LockedByLevel = 1,
        LockedByItem = 2,
        LockedByBundle = 4,
    }

    public enum SPResourceType
    {
        Item,
        Bundle,
        Currency,
        ProgressionMarker
    }
}