using System.Collections.Generic;
using Architecture.Behaviours;
using Architecture.Manager;
using Game.UI;
using UnityEngine;

namespace Game.Bubbles
{
    public class BubbleManager : ManagerBase
    {
        private readonly Dictionary<BubbleType, TextBubble> _bubbles = new Dictionary<BubbleType, TextBubble>();

        private UIContainer _container;
        
        private BubbleSettings _settings;
        
        public override void Init()
        {
            _container = Object.FindObjectOfType<UIContainer>();
            _settings = Resources.Load<BubbleSettings>("BubbleSettings"); //Костыль
        }
        
        private TextBubble CreateBubble(BubbleType type)
        {
            if (_bubbles.ContainsKey(type))
                return null;
            
            var prefab = _settings.GetBubblePrefab(type);
            var createdBubble = BehaviourProvider.Instance.InstantiateObject<TextBubble>(prefab, _container.bubblesContainer);
            _bubbles.Add(type, createdBubble);
            createdBubble.Hide();
            return createdBubble;
        }

        public void ShowBubble(BubbleType type, string text)
        {
            if (_bubbles.TryGetValue(type, out var bubble))
            {
                bubble.SetText(text);
                bubble.Show();
                
            }
            else
            {
                var newBubble = CreateBubble(type);
                newBubble.SetText(text);
                newBubble.Show();
            }
        }

        public void HideAllBubbles()
        {
            foreach (var pair in _bubbles)
                 pair.Value.Hide();
        }
    }
}
