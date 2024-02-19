using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterGameBase : SpecterResource
    {
        public SpecterGameBase() { }
        public SpecterGameBase(SPGameResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    public class SpecterGame : SpecterGameBase, ISpecterMasterObject
    {
        public string HowTo;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public List<string> DownloadUrls;
        public List<string> ScreenShotUrls;
        public List<string> PreviewVideoUrls;
        public string MinimumAppVersion;
        public bool IsGameScreenOrientationLandscape;
        public int NumberOfMatchesCreated;
        public bool IsApp;
        public bool IsDraft;
        public bool IsDefault;
        public List<SpecterGamePlatform> Platforms;
        public List<SpecterCountryDetails> Countries;
        public List<SpecterGameGenre> Genres;
        public List<SpecterMatchBase> Matches;

        public SpecterGame() { }
        public SpecterGame(SPGameResponseData data) : base(data)
        {
            HowTo = data.howTo;
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, string>();
            DownloadUrls = data.downloadUrl;
            ScreenShotUrls = data.screenShotUrl;
            PreviewVideoUrls = data.previewVideoUrl;
            MinimumAppVersion = data.minimumAppVersion;
            IsGameScreenOrientationLandscape = data.isGameScreenOrientationLandscape;
            NumberOfMatchesCreated = data.numberOfMatchesCreated;
            IsApp = data.isApp;
            IsDraft = data.isDraft;
            IsDefault = data.isDefault;
            Platforms = new List<SpecterGamePlatform>();
            foreach (var platform in data.platforms)
            {
                Platforms.Add(new SpecterGamePlatform(platform));
            }
            Countries = new List<SpecterCountryDetails>();
            foreach (var country in data.countries)
            {
                Countries.Add(new SpecterCountryDetails(country));
            }
            Genres = new List<SpecterGameGenre>();
            foreach (var genre in data.genre)
            {
                Genres.Add(new SpecterGameGenre(genre));
            }
            Matches = new List<SpecterMatchBase>();
            foreach (var match in data.matches)
            {
                Matches.Add(new SpecterMatchBase(match));
            }
        }
    }

    public class SpecterGamePlatform : SpecterAppPlatform
    {
        public string MinimumGameVersion;

        public SpecterGamePlatform(SPGamePlatformData data) : base(data)
        {
            AssetBundleUrl = data.assetBundleUrl;
            AssetBundleVersion = data.assetBundleVersion;
            MinimumGameVersion = data.minimumGameVersion;
        }
    }
}