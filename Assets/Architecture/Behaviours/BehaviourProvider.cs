using Architecture.Patterns;
using UnityEngine;

namespace Architecture.Behaviours
{
    public class BehaviourProvider : SingletonMonoBehaviour<BehaviourProvider>
    {
        public T InstantiateObject<T>(T component, Transform parent = null) where T : Object
        {
            return Instantiate(component, parent);
        }
    }
}
