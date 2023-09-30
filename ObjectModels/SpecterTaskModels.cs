using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTask : SpecterObject
    {
        public string Uuid;
        public string Id;
        public string Name;
        public string Description;
        public string IconUrl;
        
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
}