using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterGame : SpecterResource , ISpecterMasterObject
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
        public List<SpecterMatchMin> Matches;

        public SpecterGame() { }
        public SpecterGame(SPGameResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            HowTo = data.howTo;
            IconUrl = data.iconUrl;
            Tags = data.tags;
            Meta = data.meta;
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
            Matches = new List<SpecterMatchMin>();
            foreach (var match in data.matches)
            {
                Matches.Add(new SpecterMatchMin(match));
            }
        }
    }

    public class SpecterGamePlatform
    {
        public int Id;
        public string Name; 
        public string AssetBundleUrl;
        public string AssetBundleVersion;
        public string MinimumGameVersion;
        public int GamePlatformMasterId;
        
        public SpecterGamePlatform(SPGamePlatformResponseData data)
        {
            Id = data.id;
            Name = data.name;
            AssetBundleUrl = data.assetBundleUrl;
            AssetBundleVersion = data.assetBundleVersion;
            MinimumGameVersion = data.minimumGameVersion;
            GamePlatformMasterId = data.gamePlatformMasterId;
        }
    }

    public class SpecterCountryDetails
    {
        public int Id;
        public string Name;
        public string Code;

        public SpecterCountryDetails(SPCountryDetailsResponseData data)
        {
            Id = data.id;
            Name = data.name;
            Code = data.code;
        }
    }
    
    public class SpecterGameGenre
    {
        public int Id;
        public string Name;

        public SpecterGameGenre(SPGameGenreResponseData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }

 
}