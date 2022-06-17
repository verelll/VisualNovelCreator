using System;
using System.Collections.Generic;
using Architecture.Manager;
using Object = UnityEngine.Object;

namespace Game.UI
{
    public class UIManager : ManagerBase
    {
        private readonly Dictionary<Type, UIElement> _uiElements = new Dictionary<Type, UIElement>();

        private UIContainer _uiContainer;
        
        public override void Init()
        {
            _uiContainer = Object.FindObjectOfType<UIContainer>();
            InitElements();
        }

        private void InitElements()
        {
            var elements = _uiContainer.screensContainer.GetComponentsInChildren<UIElement>();
            foreach (var element in elements)
            {
                var type = element.GetType();
                _uiElements.Add(type, element);
                element.Init();
            }
        }

        public T GetElement<T>() where T : UIElement
        {
            var type = typeof(T);
            if(!_uiElements.TryGetValue(type, out var element))
                throw new Exception($"[UI Manager] Element '{type}' not found");
            
            return (T)element;
        }
    }
}
