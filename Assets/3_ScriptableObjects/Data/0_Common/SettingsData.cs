using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SettingsData", menuName = "Data/SettingsData")]
public class SettingsData : ScriptableObject
{
    public Data<float> masterVolume = new Data<float>();
}