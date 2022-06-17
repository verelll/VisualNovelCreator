using Architecture.Model;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game.Novel
{
    public class NovelButton : UIButton, IModelBehaviour
    {
        [SerializeField] protected TMP_Text buttonText;

        private NovelQuestStepModel _model;

        public void SetModel(IModel model)
        {
            if(model == null)
                return;
            
            _model = model as NovelQuestStepModel;
            
            OnButtonClickEvent -= OnClick;
            OnButtonClickEvent += OnClick;
        }

        private void OnClick()
        {
            _model?.OnClick();
        }

        public void SetText(string text)
        { 
            if(buttonText)
                buttonText.SetText(text);
        }
    }
}
