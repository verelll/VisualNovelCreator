using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Save
{
    [Serializable]
    public class SaveObject
    {
        [JsonProperty("__type", Order = -100000)]
        public string ObjectType => GetType().FullName;
    }
    
    [Serializable]
    public class SaveData 
    {
        public Dictionary<string, SaveObject> data = new Dictionary<string, SaveObject>();
    }
}
