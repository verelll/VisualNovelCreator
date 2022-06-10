using System;
using System.Collections.Generic;
using Architecture.Patterns;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(
        fileName = "UISettings",
        menuName = "UI/Settings",
        order = 0)]
    public class UISettings : SingletonScriptableObject<UISettings>
    {
        public List<WindowConfig> windows = new List<WindowConfig>();
    }

    [Serializable]
    public class WindowConfig
    {
        public UIWindowType type;
        
        public UIWindow windowPrefab;
    }


    public enum UIWindowType
    {
        None = 0,
        DialogueWindow = 10
    }
}
