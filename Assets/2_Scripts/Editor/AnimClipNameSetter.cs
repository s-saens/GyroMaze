using UnityEngine;
using UnityEditor;

public class AnimClipNameSetter : EditorWindow
{
    public static string clipName = "clip name";

    [MenuItem("SAENS/Animation Clip Name Setter")]
    private static void ShowWindow()
    {
        var window = GetWindow<AnimClipNameSetter>();
        window.titleContent = new GUIContent("AnimClipNameSetter");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        clipName = EditorGUILayout.TextField("Clip Name", clipName);
        EditorGUILayout.EndHorizontal();
    }
}