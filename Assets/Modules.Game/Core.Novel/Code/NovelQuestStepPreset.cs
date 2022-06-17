using System;
using System.Collections.Generic;
using Architecture.Model;
using Architecture.Patterns;
using UnityEngine;

namespace Game.Novel
{
    [CreateAssetMenu(
        fileName = "QuestStepPreset",
        menuName = "Game/Novel/QuestStepPreset",
        order = 0)]
    public class NovelQuestStepPreset : SingletonScriptableObject<NovelQuestStepPreset>
    {
        public List<NovelQuestStepModel> questSteps = new List<NovelQuestStepModel>();
    }
    
    [Serializable]
    public class NovelQuestStepModel : BaseModel
    {
        public string description;
        public string choice_description;
        public int id;
        public List<VisualisationProps> visualisations = new List<VisualisationProps>();
        public Card card;

        public event Action<NovelQuestStepModel> OnClickEvent;
        public void OnClick() => OnClickEvent?.Invoke(this);
    }
   
    [Serializable]
    public class VisualisationProps
    {
        public string title;
        public string description;
        public int id;
    }
   
    [Serializable]
    public class Card
    {
        public int id;
        public int queststep_id;
        public Image image;
        public DateTime updated_at;
    }
   
    [Serializable]
    public class Image
    {
        public string file_id;
    }
}
