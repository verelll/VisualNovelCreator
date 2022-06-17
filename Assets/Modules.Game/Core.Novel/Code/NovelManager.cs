using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Manager;
using Architecture.Utils;
using Game.Save;
using Game.UI;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Game.Novel
{
    public class NovelManager : ManagerBase
    {
        private UIManager _uiManager;
        private SaveManager _saveManager;
        
        private NovelMainModel _model;

        private NovelSave _dataSave;

        private NovelEdgeTaskPreset _edgeTaskPreset;
        private NovelQuestStepPreset _questStepPreset;

        public override void Init()
        {
            _edgeTaskPreset = NovelEdgeTaskPreset.Instance;
            _questStepPreset = NovelQuestStepPreset.Instance;
            
            _uiManager = GetManager<UIManager>();
            _saveManager = GetManager<SaveManager>();
           
            _model = new NovelMainModel();
            
            var isNew = !_saveManager.Contains<NovelSave>();
            _dataSave = _saveManager.Get<NovelSave>();
            
            if (isNew)
                Save(_dataSave);
            else
                Load(_dataSave);
        }

        public override void OnStart()
        {
            SetDefaultScreen();
        }

        private void Save(NovelSave dataSave)
        {
            foreach (var task in _edgeTaskPreset.tasks)
            {
                dataSave.edgeTasks.Add(new NovelEdgeTasksModel
                    {
                        target_id = task.target_id,
                        id = task.id,
                        source_id = task.source_id,
                    });
            }
            
            foreach (var task in _questStepPreset.questSteps)
            {
                var newQuestStep = new NovelQuestStepModel
                {
                    description = task.description,
                    choice_description = task.choice_description,
                    id = task.id
                };

                foreach (var visual in task.visualisations)
                {
                    var newVisual = new VisualisationProps
                    {
                        title = visual.title,
                        description = visual.description,
                        id = visual.id
                    };
                    newQuestStep.visualisations.Add(newVisual);
                }

                var newCard = new Card
                {
                    id = task.card.id,
                    image = new Image
                    {
                        file_id = task.card.image.file_id,
                    }
                };
                newQuestStep.card = newCard;
                
                dataSave.questSteps.Add(newQuestStep);
            }
        }

        private void Load(NovelSave dataSave)
        {
 
            for (var i = 0; i < dataSave.edgeTasks.Count; i++)
            {
                var taskData = dataSave.edgeTasks[i];
                _edgeTaskPreset.tasks[i] = taskData;
            }
            Debug.Log($"Data Save: {dataSave}.");
            
            for (var i = 0; i < dataSave.questSteps.Count; i++)
            {
                var questData = dataSave.questSteps[i];
               _questStepPreset.questSteps[i] = questData;
            }
        }

        private void SetDefaultScreen()
        {
            //Screen Model
            _model.CurEdgeTasksModel = _edgeTaskPreset.tasks.FirstOrDefault();

            //Cur Model
            _model.CurQuestStepModel = _questStepPreset.questSteps.FirstOrDefault();
            
            //Next data models
            var targetId = _model.CurEdgeTasksModel.target_id;
            var dataModels = GetDataModels(targetId);
            _model.nextDataModels = dataModels;

            SubscribeOnNextModels();
             var NovelScreen = _uiManager.GetElement<NovelScreen>();
             NovelScreen.SetModel(_model);
        }

        private void SubscribeOnNextModels()
        {
            foreach (var nextData in _model.nextDataModels)
                nextData.OnClickEvent += ToTheNextModel;
        }
        
        private void UnsubscribeOnNextModels()
        {
            foreach (var nextData in _model.nextDataModels)
                nextData.OnClickEvent -= ToTheNextModel;
        }

        private void ToTheNextModel(NovelQuestStepModel questStep)
        {
            UnsubscribeOnNextModels();

            var newScreenModels = GetScreenModels(questStep.card.id);
            if(newScreenModels == null)
                throw new Exception("[Game]. Next Screens not found!");
            
            _model.CurEdgeTasksModel = newScreenModels.FirstOrDefault();
            _model.CurQuestStepModel = questStep;

            var newModels = new List<NovelQuestStepModel>();
            foreach (var newScreenModel in newScreenModels)
                newModels.AddRange(GetDataModels(newScreenModel.target_id));
            
            _model.nextDataModels = newModels;
            _model.InvokeChanged();
            
            SubscribeOnNextModels();
        }
        
        private List<NovelEdgeTasksModel> GetScreenModels(int id) =>  _edgeTaskPreset.tasks.Where(screenModel => screenModel.source_id == id).ToList();

        private List<NovelQuestStepModel> GetDataModels(int id) => _questStepPreset.questSteps.Where(data => data.id == id).ToList();
    }
}
