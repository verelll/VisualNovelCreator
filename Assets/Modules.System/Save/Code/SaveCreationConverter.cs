using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using JsonReader = Newtonsoft.Json.JsonReader;

namespace Game.Save
{
    public class SaveCreationConverter : Newtonsoft.Json.Converters.CustomCreationConverter<SaveObject>
    {
        private static SaveCreationConverter _instance;

        public static SaveCreationConverter Instance => _instance ?? (_instance = new SaveCreationConverter());

        private Dictionary<string, Type> _types;

        private SaveCreationConverter()
        {
            _types = new Dictionary<string, Type>();

            var subtypes = ReflectionUtils.GetAllSubtypes<SaveObject>();
            foreach (var type in subtypes)
                _types.Add(type.FullName, type);
        }

        public override SaveObject Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public SaveObject Create(Type objectType, JObject jObject)
        {
            var type = (string) jObject.Property("__type");

            if (_types.TryGetValue(type, out var TargetType))
            {
                return (SaveObject) Activator.CreateInstance(TargetType);
            }

            throw new ApplicationException(String.Format("The given vehicle type {0} is not supported!", type));
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        
    }
}