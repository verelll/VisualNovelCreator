using Architecture.Behaviours;
using TMPro;
using UnityEngine;

namespace Game.Bubbles
{
    public class TextBubble : BaseMonoBehaviour
    {
        public BubbleType bubbleType;

        public TMP_Text text;
        
        public Transform content;
        
        public bool IsShow { get; private set; }
        
        public bool IsHide { get; private set; }

        public void Show()
        {
            IsShow = true;
            IsHide = false;

            content.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            IsShow = false;
            IsHide = true;

            content.gameObject.SetActive(false);
        }

        public void SetText(string text)
        {
            this.text.SetText(text);
        }
    }
}
