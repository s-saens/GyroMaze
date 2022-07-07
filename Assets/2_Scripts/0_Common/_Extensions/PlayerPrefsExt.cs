using UnityEngine;
using Newtonsoft.Json;

public static class PlayerPrefsExt
{
    public static void SetObject<T>(string key, T value)
    {
        PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T GetObject<T>(string key)
    {
        string value = PlayerPrefs.GetString(key, null);
        return JsonConvert.DeserializeObject<T>(value);
    }
}