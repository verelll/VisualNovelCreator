using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Architecture.Utils
{
    public static class JsonReader
    {
        private const string JSON_FOLDER = "/JsonAssets";
        
        public static string FindJsonFile(string fileName)
        {
            var path = Path.Combine(Application.dataPath + JSON_FOLDER, fileName);
            Debug.Log(path);
            if (!File.Exists(path)) 
                throw new Exception("JSON FILE NOT FOUND.");
            
            var content = File.ReadAllText(path);
            return content;
        }
        
        public static List<T> DeserializeFileList<T>(string file) => JsonConvert.DeserializeObject<List<T>>(file);
    }
}
