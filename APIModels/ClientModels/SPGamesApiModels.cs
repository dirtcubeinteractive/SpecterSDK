using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Api Data Models

    [Serializable]
    public class SPGameResponseBaseData : SPResourceResponseData { }

    [Serializable]
    public class SPGameResponseData : SPGameResponseBaseData, ISpecterMasterData
    {
        public string howTo { get; set; }
        public List<string> downloadUrl { get; set; }
        public List<string> screenShotUrl { get; set; }
        public List<string> previewVideoUrl { get; set; }
        public string minimumAppVersion { get; set; }
        public bool isGameScreenOrientationLandscape { get; set; }
        public int numberOfMatchesCreated { get; set; }
        public bool isApp { get; set; }
        public bool isDraft { get; set; }
        public bool isDefault { get; set; }
        public List<SPGamePlatformData> platforms { get; set; }
        public List<SPCountryDetailsData> countries { get; set; }
        public List<SPGameGenreData> genre { get; set; }
        public List<SPMatchResponseBaseData> matches { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPGetGamesResponseData : ISpecterApiResponseData
    {
        public List<SPGameResponseData> games { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPGamePlatformData : SPAppPlatformData
    {
        public string minimumGameVersion { get; set; }
    }

    #endregion
}