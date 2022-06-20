#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class AnimationClipNameSetter : EditorWindow
{
    public static string clipName = "clip name";

    [MenuItem("SAENS/Animation Clip Name Setter")]
    private static void ShowWindow()
    {
        var window = GetWindow<AnimationClipNameSetter>();
        window.titleContent = new GUIContent("Animation Clip Name Setter");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        clipName = EditorGUILayout.TextField("Clip Name", clipName);
        EditorGUILayout.EndHorizontal();
    }
}
#endif