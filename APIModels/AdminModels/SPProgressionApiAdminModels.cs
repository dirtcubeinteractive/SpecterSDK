using System;
using System.Collections.Generic;

namespace SpecterSDK.APIModels.AdminModels
{
    [Serializable]
    public class SPProgressionSystemAdminData
    {
        public string id;
        public string levelSystemId;
        public string name;
        public int progressionMarkerId;
        public SPProgressionMarkerAdminData progressionMarker;
        public List<SPProgressionSystemLevelAdminData> levelSystemLevelMapping;
    }

    [Serializable]
    public class SPProgressionSystemLevelAdminData
    {
        public string id;
        public int levelNo;
        public int parameterValue;
    }

    [Serializable]
    public class SPProgressionMarkerAdminData
    {
        public int id;
        public string progressionMarkerId;
        public string name;
    }
}