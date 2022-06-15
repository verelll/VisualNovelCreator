using UnityEngine;

namespace Architecture.Utils
{
    public static class ResourceLoader
    {
        public static Sprite LoadImageFromName(string name) => Resources.Load<Sprite>(name);
        
        public static T Load<T>(string path = "") where T: ScriptableObject => Resources.Load<T>(path);
    }
}
