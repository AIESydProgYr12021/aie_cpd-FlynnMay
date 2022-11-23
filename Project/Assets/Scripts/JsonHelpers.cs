using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class JsonHelpers
{
    public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false, TypeNameHandling typeNameHandling = TypeNameHandling.Auto) where T : new()
    {
        TextWriter writer = null;
        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = typeNameHandling;
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, settings);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    public static T ReadFromJsonFile<T>(string filePath, TypeNameHandling typeNameHandling = TypeNameHandling.Auto) where T : new()
    {
        TextReader reader = null;
        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = typeNameHandling;
            reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileContents, settings);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }
}
