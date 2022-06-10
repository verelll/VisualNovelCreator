using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Architecture.Patterns
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    var type = typeof(T);
                    var paths = AssetDatabase.GetAllAssetPaths();
                    var path  = paths.FirstOrDefault(p => p.EndsWith(type.Name + ".asset"));
                    _instance = (T)AssetDatabase.LoadAssetAtPath(path, type);

                    _instance.Initialize();
                }
                return _instance;
            }
        }
        
        protected virtual void Initialize()
        {
			
        }
    }
}