using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class SaveLoadJson
{
    public static void Create<T>(string fileName, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);
    }
    public static void Create<T>(string fileName, T[] data)
    {
        int length = data.Length;
        string json = "";
        for (int i = 0; i < length; i++)
            json += JsonUtility.ToJson(data[i], true);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);
    }

    public static void Update<T>(string fileName, T data)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
        JsonUtility.FromJsonOverwrite(json, (T)data);
    }
    public static T Load<T>(string fileName)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
        return JsonUtility.FromJson<T>(json);
    }
    public static T[] LoadArray<T>(string fileName)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
        json = json.Replace('{','}');
        string []arrayString =  json.Split('}');
        List<T> result = new List<T>();
        foreach (var item in arrayString)
        {
            if(item.Equals("")) continue;
            string convertToJson = "{"+item+"}";
            result.Add(JsonUtility.FromJson<T>(convertToJson));
        }
        return result.ToArray();
    }
    public static Dictionary<T1,T2> LoadDictionary<T1,T2>(string fileName)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
        json = json.Replace('{', '}');
        string[] arrayString = json.Split('}');
        Dictionary<T1, T2> result = new Dictionary<T1, T2>();

        string convertToJson = "";
        foreach (var item in arrayString)
        {
            if (item.Equals("")) continue;
            convertToJson += "{" + item + "}";
        }

        return result = (Dictionary<T1, T2>)JsonUtility.FromJson(convertToJson, typeof(Dictionary<T1, T2>));
    }
}

