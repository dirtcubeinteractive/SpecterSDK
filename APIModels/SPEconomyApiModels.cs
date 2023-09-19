namespace SpecterSDK.APIModels
{
    [System.Serializable]
    public abstract class SPResourceObject : SPCustomizableObject { }
    
    [System.Serializable]
    public class SPCurrencyData : SPResourceObject
    {
        public string code { get; set; }
    }

    [System.Serializable]
    public class SPItemData : SPResourceObject
    {
        
    }

    [System.Serializable]
    public class SPBundleData : SPResourceObject
    {
        
    }
}