using System.Collections.Generic;
using Architecture.Model;

namespace Game.Novel
{
    public class NovelMainModel : BaseModel
    {
        public NovelEdgeTasksModel CurEdgeTasksModel;
        public NovelQuestStepModel CurQuestStepModel;
        public List<NovelQuestStepModel> nextDataModels;
    }
}