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
    public static T GetObject<T>(string key)
    {
        string value = PlayerPrefs.GetString(key, null);
        return JsonConvert.DeserializeObject<T>(value);
    }
}