using System.Collections.Generic;
using Game.Save;

namespace Game.Novel
{
    public class NovelSave : SaveObject
    {
        public List<NovelEdgeTasksModel> edgeTasks = new List<NovelEdgeTasksModel>();
        public List<NovelQuestStepModel> questSteps = new List<NovelQuestStepModel>();
    }
}