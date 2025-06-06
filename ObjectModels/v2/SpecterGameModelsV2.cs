using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPApp : ISpecterResource, ISpecterGame, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        
        public List<SPAppPlatformInfo> Platforms { get; set; }
        public List<SpecterLocation> Locations { get; set; }
        public List<SpecterGameGenre> Genres { get; set; }
        public List<SpecterAppCategory> Categories { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SPApp(SPAppInfoData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            HowTo = data.howTo;
            ScreenshotUrls = data.screenshotUrls ?? new List<string>();
            VideoUrls = data.videoUrls ?? new List<string>();
            Platforms = data.platforms == null ? new List<SPAppPlatformInfo>() : data.platforms.ConvertAll(x => new SPAppPlatformInfo(x));
            Locations = data.locations == null ? new List<SpecterLocation>() : data.locations.ConvertAll(x => new SpecterLocation(x));
            Genres = data.genres == null ? new List<SpecterGameGenre>() : data.genres.ConvertAll(x => new SpecterGameGenre(x));
            Categories = data.categories == null ? new List<SpecterAppCategory>() : data.categories.ConvertAll(x => new SpecterAppCategory(x));
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SPGame : ISpecterResource, ISpecterGame, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public List<SPAppPlatformInfo> Platforms { get; set; }
        public List<SpecterLocation> Locations { get; set; }
        public List<SpecterGameGenre> Genres { get; set; }
        
        public bool IsLandscape { get; set; }
        public List<SPMatchResource> Matches { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SPGame(SPGameData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            HowTo = data.howTo;
            ScreenshotUrls = data.screenshotUrls;
            VideoUrls = data.videoUrls;
            Platforms = data.platforms?.ConvertAll(x => new SPAppPlatformInfo(x));
            Locations = data.locations?.ConvertAll(x => new SpecterLocation(x));
            Genres = data.genres?.ConvertAll(x => new SpecterGameGenre(x));
            
            IsLandscape = data.isScreenOrientationLandscape;
            Matches = data.matches?.ConvertAll(x => new SPMatchResource(x));
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }
}