using System.Collections.Generic;
using Architecture.Model;

namespace Game.Novel
{
    public class NovelMainModel : BaseModel
    {
        public NovelEdgeTasksModel curEdgeTasksModel;
        public NovelQuestStepModel curQuestStepModel;
        public List<NovelQuestStepModel> nextQuestStepModels;
    }
}