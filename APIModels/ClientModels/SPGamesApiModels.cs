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
        public bool? isGameScreenOrientationLandscape { get; set; }
        public int numberOfMatchesCreated { get; set; }
        public bool isApp { get; set; }
        public bool isDraft { get; set; }
        public bool isDefault { get; set; }
        public List<SPGamePlatformData> platforms { get; set; }
        public List<SPCountryDetailsData> countries { get; set; }
        public List<SPGameGenreData> genre { get; set; }
        public List<SPMatchResponseBaseData> matches { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    [Serializable]
    public class SPGameResponseDataList : SPResponseDataList<SPGameResponseData> { }

    [Serializable]
    public class SPGamePlatformData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string assetBundleUrl { get; set; }
        public string assetBundleVersion { get; set; }
        public string minimumGameVersion { get; set; }
        public int gamePlatformMasterId { get; set; }
    }

    [Serializable]
    public class SPCountryDetailsData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    [Serializable]
    public class SPGameGenreData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
       
   

    #endregion
}