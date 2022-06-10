using System.IO;
using UnityEngine;

namespace Architecture.Utils
{
    public static class JsonFormator
    {
        public const string SAVE_FILE_NAME = "save.json";

        public static string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        
    }
}
