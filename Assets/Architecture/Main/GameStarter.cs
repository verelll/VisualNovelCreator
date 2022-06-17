using System.Collections;
using Architecture.IOC;
using Game.Bubbles;
using Game.Novel;
using Game.Save;
using Game.UI;
using UnityEngine;

namespace Architecture.Starter
{
    public class GameStarter : MonoBehaviour
    {
        [HideInInspector]
        public Container container;
        
        private void Start()
        {
            container = GlobalContainer.Main;

            container.Add<SaveManager>();
            
            container.Add<UIManager>();
            container.Add<NovelManager>();
            container.Add<BubbleManager>();

            StartCoroutine(StartEpics());
        }

        private IEnumerator StartEpics()
        {
            foreach (var manager in container.ManagerObjects)
            {
                manager.Init();
                yield return null;
            }
            
            foreach (var manager in container.ManagerObjects)
            {
                manager.OnStart();
                yield return null;
            }
            
            yield return null;
        }

        private void OnApplicationQuit()
        {
            Debug.Log("Application ending after " + Time.time + " seconds");
            container.Dispose();
        }
    }
}
