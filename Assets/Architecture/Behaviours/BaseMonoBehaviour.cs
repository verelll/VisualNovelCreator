using System.Collections.Generic;
using Architecture.Manager;
using Architecture.Starter;
using UnityEngine;

namespace Architecture.Behaviours
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        protected T GetManager<T>() where T : IManager => GameStarter.instance.GetManager<T>();

        protected List<T> GetAll<T>(List<T> objects = null) => GameStarter.instance.GetAll<T>(objects);
    }
}
