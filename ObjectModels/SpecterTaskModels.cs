using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTask : SpecterResource
    {
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
    
    public class SpecterTaskCollection : SpecterObjectList<SpecterTask> { }

    public class SpecterTaskGroup : SpecterResource
    {
        
    }
}