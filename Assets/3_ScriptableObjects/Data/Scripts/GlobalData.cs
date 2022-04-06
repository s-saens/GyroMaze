using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Data/GlobalData")]
public class GlobalData : ScriptableObject
{
    public Data<int> chapterIndex = new Data<int>();
    public Data<int> stageIndex = new Data<int>();
}