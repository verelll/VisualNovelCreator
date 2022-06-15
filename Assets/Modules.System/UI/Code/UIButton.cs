using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        public event Action OnButtonClickEvent;
        
        private void Start() =>  button?.onClick.AddListener(OnButtonClick);

        private void OnDestroy() => button?.onClick.RemoveListener(OnButtonClick);
        
        public void SetInteractable(bool active) => button.interactable = active;

        private void OnButtonClick() => OnButtonClickEvent?.Invoke();
    }
}
