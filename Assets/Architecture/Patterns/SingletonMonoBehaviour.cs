using UnityEngine;

namespace Architecture.Patterns
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject o = new GameObject("EventProvider");
                    _instance = o.AddComponent<T>();
                    DontDestroyOnLoad(o);
                }

                return _instance;
            }
        }

    }
}

