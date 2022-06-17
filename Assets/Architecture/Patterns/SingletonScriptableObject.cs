using System.Linq;
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
                    _instance = Resources.LoadAll<T>("").First();
                }

                return _instance;
            }
        }
    }
}
