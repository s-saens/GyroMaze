using UnityEngine;
using System;

public static class GameData
{
    public static Data<int> stageIndex = new Data<int>();
    public static int lastStage = 0;

    public static class Settings
    {
        public static Data<float> masterVolume = new Data<float>();
    }
}