using System;
using System.Collections.Generic;
using Architecture.Model;
using Architecture.Patterns;
using UnityEngine;

namespace Game.Novel
{
    [CreateAssetMenu(
        fileName = "EdgeTasksPreset",
        menuName = "Game/Novel/EdgeTasksPreset",
        order = 0)]
    public class NovelEdgeTaskPreset : SingletonScriptableObject<NovelEdgeTaskPreset>
    {
        public List<NovelEdgeTasksModel> tasks = new List<NovelEdgeTasksModel>();
    }
    
    [Serializable]
    public class NovelEdgeTasksModel: BaseModel
    {
        public int source_id;
        public int target_id;
        public object id;
    }
}