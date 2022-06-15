using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Architecture.Manager;
using Game.Bubbles;
using Game.Novel;
using Game.UI;
using UnityEngine;

namespace Architecture.Starter
{
    public class GameStarter : MonoBehaviour
    {
        public static GameStarter instance;

        private readonly Dictionary<Type, IManager> _managers = new Dictionary<Type, IManager>();

        private bool Inited;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            AddManager<UIManager>();
            AddManager<NovelManager>();
            AddManager<BubbleManager>();
            
            Inited = true;

            StartCoroutine(StartEpics());
        }

        private IEnumerator StartEpics()
        {
            foreach (var pair in _managers)
            {
                pair.Value.Init();
                yield return null;
            }
            
            foreach (var pair in _managers)
            {
                pair.Value.OnStart();
                yield return null;
            }
            
            yield return null;
        }

        #region Get

        public T GetManager<T>() where T : IManager
        {
            var type = typeof(T);
            if (!_managers.TryGetValue(type, out var manager))
                throw new Exception($"[Game]. Manager [{type}] not found");
               
            return (T) manager;
        }

        public List<A> GetAll<A>(List<A> objects = null)
        {
            Type target = typeof(A);
            
            if (objects == null)
                objects = new List<A>();
            
            foreach (var manager in _managers.Values)
            {
                if (manager.GetType().GetInterfaces().Contains(target))
                {
                    objects.Add((A) manager);
                }
            }

            return objects;
        }

        #endregion
        
        #region Add Manager

        private T AddManager<T>() where T : IManager, new()
        {
            var type = typeof(T);
            var manager = new T();
            _managers.Add(type, manager);
            return manager;
        }
        
        #endregion
    }
}
