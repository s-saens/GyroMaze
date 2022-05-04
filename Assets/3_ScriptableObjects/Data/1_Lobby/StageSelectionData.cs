using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StageSelectionData", menuName = "Data/StateSelectionData")]
public class StageSelectionData : ScriptableObject
{
    public int stageCount = 50;
    public int visibleStageCount = 3;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int minSize = 400;
    public float stopTime = 300;
    public float magnetLerpTime = 10;
}