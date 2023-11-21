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
        public List<SpecterCountryDetail> Countries;
        public List<SpecterGameGenre> Genres;
        public List<SpecterGameMatch> Matches;

        public SpecterGame() { }
        public SpecterGame(SPGameResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            HowTo = data.howTo;
            IconUrl = data.iconUrl;
            Tags = new List<string>();
            Tags = data.tags;
            Meta = new Dictionary<string, string>();
            Meta = data.meta;
            DownloadUrls = new List<string>();
            DownloadUrls = data.downloadUrl;
            ScreenShotUrls = new List<string>();
            ScreenShotUrls = data.screenShotUrl;
            PreviewVideoUrls = new List<string>();
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
            Countries = new List<SpecterCountryDetail>();
            foreach (var country in data.countries)
            {
                Countries.Add(new SpecterCountryDetail(country));
            }
            Genres = new List<SpecterGameGenre>();
            foreach (var genre in data.genre)
            {
                Genres.Add(new SpecterGameGenre(genre));
            }
            Matches = new List<SpecterGameMatch>();
            foreach (var match in data.matches)
            {
                Matches.Add(new SpecterGameMatch(match));
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

    public class SpecterCountryDetail
    {
        public int Id;
        public string Name;
        public string Code;

        public SpecterCountryDetail(SPCountryDetailResponseData data)
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
        public string Code;

        public SpecterGameGenre(SPGameGenreResponseData data)
        {
            Id = data.id;
            Name = data.name;
            Code = data.code;
        }
    }

    public class SpecterGameMatch : SpecterResource
    {
        public string HowTo;
        public int MinPlayers;
        public int MaxPlayers;
        public int NumberOfPosition;
        public int DefaultOutcomeValue;
        public SpecterGameMatchFormat MatchFormatType;
        public SpecterGameMatchOutcome MatchOutcomeType;
        
        public SpecterGameMatch(SPGameMatchResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            HowTo = data.howTo;
            IconUrl = data.iconUrl;
            MinPlayers = data.minPlayers;
            MaxPlayers = data.maxPlayers;
            NumberOfPosition = data.numberOfPosition;
            DefaultOutcomeValue = data.defaultOutcomeValue;
            MatchFormatType = new SpecterGameMatchFormat(data.matchFormatType);
            MatchOutcomeType = new SpecterGameMatchOutcome(data.matchOutcomeType);
        }
    }

    public class SpecterGameMatchFormat 
    {
        public int Id;
        public string Name;

        public SpecterGameMatchFormat(SPGameMatchFormatResponseData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
    
    public class SpecterGameMatchOutcome
    {
        public int Id;
        public string Name;

        public SpecterGameMatchOutcome(SPGameMatchOutcomeResponseData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
    
}