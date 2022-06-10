using System.Collections;
using System.Collections.Generic;
using System.IO;
using Architecture.Manager;
using UnityEngine;

namespace Game.Save
{
    public class SaveManager : ManagerBase
    {
        public const string SAVE_FILE_NAME = "save.json";

        public static string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        
        public override void Init()
        {
            var managers = GetAll<ISaveHandler>();
            
        }

        public override void Dispose()
        {
            base.Dispose();
        }
        
        
    }
}
