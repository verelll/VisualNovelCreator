using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Bubbles
{
    public enum BubbleType
    {
        None = 0,
        Mental = 10,
        Author = 20
    }
    
    [CreateAssetMenu(
        fileName = "BubbleSettings",
        menuName = "Game/Bubbles/Settings",
        order = 0)]
    public class BubbleSettings : ScriptableObject
    {
        public List<BubblePropsSettings> bubbles = new List<BubblePropsSettings>();
        
        public TextBubble GetBubblePrefab(BubbleType type)
        {
            var bubblesDict = bubbles.ToDictionary(x => x.type, a => a.textBubblePrefab);  //Велосипед для удобства, поскольку нет Odin'a
            if(!bubblesDict.TryGetValue(type, out var prefab))
                throw new Exception($"[Bubble Settings] Key '{type}' not found!");

            return prefab;
        }
    }

    [System.Serializable]
    public class BubblePropsSettings
    {
        public BubbleType type;

        public TextBubble textBubblePrefab;
    }
}
