using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;

namespace SpecterSDK.ObjectModels.v1
{
    public class SpecterApp : SpecterResource, ISpecterMasterObject
    {
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public SPAppCategory Categories { get; set; }
        public List<SpecterAppPlatform> Platforms { get; set; }
        public List<SPLocation> Countries { get; set; }
        public List<SPGenre> Genres { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterApp(SPAppInfoResponseData data) : base(data)
        {
            HowTo = data.howTo;
            ScreenshotUrls = data.screenshotUrls ?? new List<string>();
            VideoUrls = data.videoUrls ?? new List<string>();
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
            
            Categories = new SPAppCategory(data.categories);

            Platforms = new List<SpecterAppPlatform>();
            if (data.platforms != null)
            {
                foreach (var platform in data.platforms)
                {
                    Platforms.Add(new SpecterAppPlatform(platform));
                }
            }

            Countries = new List<SPLocation>();
            if (data.countries != null)
            {
                foreach (var country in data.countries)
                {
                    Countries.Add(new SPLocation(country));
                }
            }

            Genres = new List<SPGenre>();
            if (data.genre != null)
            {
                foreach (var genre in data.genre)
                {
                    Genres.Add(new SPGenre(genre));
                }
            }
        }
    }

    public class SPAppCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SPAppCategory(SPAppCategoryData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
}