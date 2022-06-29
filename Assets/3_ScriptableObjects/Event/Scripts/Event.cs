using UnityEngine;
using System;

public class Event : ScriptableObject
{
    public Action<string> callback;
}