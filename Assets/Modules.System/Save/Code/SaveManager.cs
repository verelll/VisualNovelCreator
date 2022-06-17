using System.IO;
using Architecture.Manager;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Game.Save
{
    public class SaveManager : ManagerBase
    {
        public const string SAVE_FILE_NAME = "save.json";

		public static string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

		public SaveData SaveData { get; private set; }
		
		public override void Init()
		{
			Load();
		}

		public override void Dispose()
		{
			Save();
		}

		public void Save()
		{
			string content = JsonConvert.SerializeObject(
				SaveData,
				new JsonSerializerSettings{ ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
			);
			File.WriteAllText(SavePath, content);
		}

		public void Load()
		{
			if (File.Exists(SavePath))
			{
				string content = File.ReadAllText(SavePath);
				SaveData = JsonConvert.DeserializeObject<SaveData>(content, SaveCreationConverter.Instance);
			}
			if(SaveData == null)
            {
				SaveData = new SaveData();
			}
		}

		public bool Contains<T>() where T : SaveObject
		{
			var name = typeof(T).Name;
			
			return SaveData.data.ContainsKey(name);
		}

		public T Get<T>() where T : SaveObject, new()
		{
			var name = typeof(T).Name;
			
			if (!SaveData.data.TryGetValue(name, out var save))
			{
				save = new T();
				SaveData.data.Add(name, save);
			}

			return save as T;
		}

#if UNITY_EDITOR

		[MenuItem("Saves/Open saves folder", priority = 10)]
		public static void OpenSavesFolder()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}

		[MenuItem("Saves/Delete save", priority = 12)]
		public static void DeleteSave()
		{
			if (File.Exists(SavePath))
				File.Delete(SavePath);
		}

#endif
    }
}
