using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Api Data Models

    [Serializable]
    public class SPGameResponseData : ISpecterApiResponseData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string howTo { get; set; }
        public string iconUrl { get; set; }
        public List<string> downloadUrl { get; set; }
        public List<string> screenShotUrl { get; set; }
        public List<string> previewVideoUrl { get; set; }
        public string minimumAppVersion { get; set; }
        public bool? isGameScreenOrientationLandscape { get; set; }
        public int numberOfMatchesCreated { get; set; }
        public bool isApp { get; set; }
        public bool isDraft { get; set; }
        public bool isDefault { get; set; }
        public List<SPGamePlatformResponseData> platforms { get; set; }
        public List<SPCountryDetailsResponseData> countries { get; set; }
        public List<SPGameGenreResponseData> genre { get; set; }
        public List<SPMatchMinResponseData> matches { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        
        // TODO: Add leaderboards list
    }

    [Serializable]
    public class SPGameResponseDataList : SPResponseDataList<SPGameResponseData> { }

    [Serializable]
    public class SPGamePlatformResponseData : ISpecterApiResponseData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string assetBundleUrl { get; set; }
        public string assetBundleVersion { get; set; }
        public string minimumGameVersion { get; set; }
        public int gamePlatformMasterId { get; set; }
    }

    [Serializable]
    public class SPCountryDetailsResponseData : ISpecterApiResponseData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    [Serializable]
    public class SPGameGenreResponseData : ISpecterApiResponseData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
       
   

    #endregion
}