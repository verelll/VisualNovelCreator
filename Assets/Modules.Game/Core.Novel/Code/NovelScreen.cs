using System.Collections.Generic;
using System.Linq;
using Architecture.Model;
using Architecture.Utils;
using Game.Bubbles;
using Game.UI;
using UnityEngine;

namespace Game.Novel
{
    public class NovelScreen : UIElement
    {
        [SerializeField] private UnityEngine.UI.Image backgroundImage;
        [SerializeField] private NovelButton backgroundButton;

        [SerializeField] private List<NovelButton> buttons = new List<NovelButton>();

        private BubbleManager _bubbleManager;

        private NovelMainModel _model;

        public override void Init()
        {
            _bubbleManager = GetManager<BubbleManager>();
        }

        public override void SetModel(IModel model)
        {
            if (_model != null)
                _model.OnChanged -= UpdateModel;
            
            _model = model as NovelMainModel;
            _model.OnChanged += UpdateModel;
           UpdateModel();
        }

        private void UpdateModel()
        {
            if(_model == null)
                return;
            
            UpdateBackground();
            UpdateDescription();
            UpdateButtons();
        }

        private void UpdateBackground()
        {
            var background = _model.CurQuestStepModel.card.image.file_id;
            backgroundImage.sprite = ResourceLoader.LoadImageFromName(background);
        }
        
        private void UpdateDescription()
        {
            _bubbleManager.HideAllBubbles();
 
            //Очень костыльный костыль
            var visualId = _model.CurQuestStepModel.visualisations.FirstOrDefault().id;
            if (visualId == 5)
                 _bubbleManager.ShowBubble(BubbleType.Mental, _model.CurQuestStepModel.description);
            else if(visualId == 7)
                 _bubbleManager.ShowBubble(BubbleType.Author, _model.CurQuestStepModel.description);
        }

        private void UpdateButtons()
        {
            HideButtons();
            
            if (_model.nextDataModels.Count > 1)
            {
                for (int i = 0; i < _model.nextDataModels.Count; i++)
                {
                    var button = buttons[i];
                    var model = _model.nextDataModels[i];

                    button.SetModel(model);
                    button.SetText(model.choice_description);
                    button.gameObject.SetActive(true);
                }
            }
            else
            {
                var dataModel = _model.nextDataModels.FirstOrDefault();
                
                backgroundButton.SetModel(dataModel);
                backgroundButton.SetInteractable(true);
            }

            void HideButtons()
            {
                foreach (var button in buttons)
                    button.gameObject.SetActive(false);
                
                backgroundButton.SetInteractable(false);
            }
        }
    }
}
