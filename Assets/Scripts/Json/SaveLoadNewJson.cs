using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class SaveLoadNewJson
{
    //private static Dictionary<string, int> listCurrentGun;
    public static void Save<T>(string keyName, T data)
    {
        string json = JsonConvert.SerializeObject(data);

        CPlayerPrefs.SetString(keyName, json);
    }
    public static T Load<T>(string keyName)
    {
        return JsonConvert.DeserializeObject<T>(CPlayerPrefs.GetString(keyName));
    }
}

