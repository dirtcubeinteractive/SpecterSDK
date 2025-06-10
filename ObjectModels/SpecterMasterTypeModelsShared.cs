using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.ObjectModels
{
    public class SPGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SPGenre() { }
        public SPGenre(SPGenreData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }

    public class SPLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SPLocation() { }
        public SPLocation(SPLocationData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
}