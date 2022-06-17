using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Manager;
using Architecture.Utils;
using Game.UI;
using UnityEngine;

namespace Game.Novel
{
    public class NovelManager : ManagerBase
    {
        #region Const

        private const string EDGE_NAME = "edge_task.json";
        
        private const string QUEST_STEP_NAME = "quest_step_task.json";

        #endregion

        private List<NovelScreenModel> _screenModels = new List<NovelScreenModel>();
        private List<NovelDataModel> _screenDataModels = new List<NovelDataModel>();
        
        private UIManager _uiManager;
        
        private NovelMainModel _model;

        public override void Init()
        {
            _model = new NovelMainModel();
            
            _uiManager = GetManager<UIManager>();
            LoadSettings();
           
        }

        public override void OnStart()
        {
            SetDefaultScreen();
        }

        private void LoadSettings()
        {
            _screenModels.Clear();
            _screenDataModels.Clear();
            
            Debug.Log(EDGE_NAME);
            //Перенести в сейв менеджер
            var screenFile = JsonReader.FindJsonFile(EDGE_NAME);
            var screenModels =  JsonReader.DeserializeFileList<NovelScreenModel>(screenFile);
            _screenModels.AddRange(screenModels);
            
            var dataFile = JsonReader.FindJsonFile(QUEST_STEP_NAME);
            var screenDataModels = JsonReader.DeserializeFileList<NovelDataModel>(dataFile);
            _screenDataModels.AddRange(screenDataModels);
        }

        private void SetDefaultScreen()
        {
            //Screen Model
            _model.curScreenModel = _screenModels.FirstOrDefault();

            //Cur Model
            _model.curDataModel = _screenDataModels.FirstOrDefault();
            
            //Next data models
            var targetId = _model.curScreenModel.target_id;
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

        private void ToTheNextModel(NovelDataModel data)
        {
            UnsubscribeOnNextModels();

            var newScreenModels = GetScreenModels(data.card.id);
            if(newScreenModels == null)
                throw new Exception("[Game]. Next Screens not found!");
            
            _model.curScreenModel = newScreenModels.FirstOrDefault();
            _model.curDataModel = data;

            var newModels = new List<NovelDataModel>();
            foreach (var newScreenModel in newScreenModels)
                newModels.AddRange(GetDataModels(newScreenModel.target_id));
            
            _model.nextDataModels = newModels;
            _model.InvokeChanged();
            
            SubscribeOnNextModels();
        }
        
        private List<NovelScreenModel> GetScreenModels(int id) =>  _screenModels.Where(screenModel => screenModel.source_id == id).ToList();

        private List<NovelDataModel> GetDataModels(int id) => _screenDataModels.Where(data => data.id == id).ToList();
    }
}
