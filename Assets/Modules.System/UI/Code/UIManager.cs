using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Manager;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.UI
{
    public class UIManager : ManagerBase
    {
        private readonly Dictionary<Type, UIWindow> _windows = new Dictionary<Type, UIWindow>();

        private UIContainer _uiContainer;
        
        public override void Init()
        {
            _uiContainer = FindObjectOfType<UIContainer>();
            InitWindows();
        }

        private void InitWindows()
        {
            var windows = _uiContainer.windowContainer.GetComponentsInChildren<UIWindow>();
            foreach (var window in windows)
            {
                var type = window.GetType();
                _windows.Add(type, window);
                window.Init();
            }
        }
    }
}
