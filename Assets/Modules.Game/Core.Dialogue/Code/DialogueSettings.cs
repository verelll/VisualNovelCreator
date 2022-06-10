using System;
using System.Collections.Generic;
using Architecture.Patterns;
using UnityEngine;

namespace Game.Dialogue
{
    [CreateAssetMenu(
        fileName = "DialogueSettings",
        menuName = "Dialogue/Settings",
        order = 0)]
    public class DialogueSettings : SingletonScriptableObject<DialogueSettings>
    {
        public List<DialogueGroup> nodes = new List<DialogueGroup>();
    }

    [Serializable]
    public class DialogueGroup
    {
        [Space]
        public int id;

        [TextArea(5, 15)]
        public string description;
        
        [TextArea(2, 5)]
        public string choice_description;

        public DialogueVisual visualisation;

        public DialogueCard card;
    }

    [Serializable]
    public class DialogueVisual
    {
        public int id;
        
        public string title;
        
        public string description;
    }

    [Serializable]
    public class DialogueCard
    {
        public int id;
        
        public int queststep_id;

        public Sprite icon;

        public string updated_At;
    }
}
