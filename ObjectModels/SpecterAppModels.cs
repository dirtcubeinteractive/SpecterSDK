using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterApp : SpecterResource, ISpecterMasterObject
    {
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public SpecterAppCategory Categories { get; set; }
        public List<SpecterAppPlatform> Platforms { get; set; }
        public List<SpecterLocation> Countries { get; set; }
        public List<SpecterGameGenre> Genres { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterApp(SPAppInfoResponseData data) : base(data)
        {
            HowTo = data.howTo;
            ScreenshotUrls = data.screenshotUrls ?? new List<string>();
            VideoUrls = data.videoUrls ?? new List<string>();
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
            
            Categories = new SpecterAppCategory(data.categories);

            Platforms = new List<SpecterAppPlatform>();
            if (data.platforms != null)
            {
                foreach (var platform in data.platforms)
                {
                    Platforms.Add(new SpecterAppPlatform(platform));
                }
            }

            Countries = new List<SpecterLocation>();
            if (data.countries != null)
            {
                foreach (var country in data.countries)
                {
                    Countries.Add(new SpecterLocation(country));
                }
            }

            Genres = new List<SpecterGameGenre>();
            if (data.genre != null)
            {
                foreach (var genre in data.genre)
                {
                    Genres.Add(new SpecterGameGenre(genre));
                }
            }
        }
    }

    public class SpecterAppCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SpecterAppCategory(SPAppCategoryData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
}