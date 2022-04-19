using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(_EventButton))]
public class _EventButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif