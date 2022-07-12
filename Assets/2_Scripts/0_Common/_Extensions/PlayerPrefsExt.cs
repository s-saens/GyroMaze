using UnityEngine;
using Newtonsoft.Json;

public static class PlayerPrefsExt
{
    public static void SetObject<T>(string key, T value)
    {
        var settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value, settings));
    }
    public static T GetObject<T>(string key, T defaultValue = default(T))
    {
        if(!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"NO PREFS OF KEY {key}");
            return defaultValue;
        }
        string value = PlayerPrefs.GetString(key, "");
        if(value == "")
        {
            return defaultValue;
        }
        else
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
    public static string GetObjectRaw(string key)
    {
        return PlayerPrefs.GetString(key, null);
    }
}