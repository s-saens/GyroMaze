//C# Example
using UnityEditor;
using UnityEngine;
using TMPro;

public class LevelButtonGenerator : EditorWindow
{
    private int buttonsCount = 30;
    private GameObject levelButtonPrefab;
    private Transform parentTransform;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("SAENS/LevelButtonGenerator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(LevelButtonGenerator));
    }

    private void OnGUI()
    {

        EditorGUILayout.BeginHorizontal();

        buttonsCount = EditorGUILayout.IntField("Buttons Count", buttonsCount);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        levelButtonPrefab = (GameObject)EditorGUILayout.ObjectField("Button Prefab", levelButtonPrefab, typeof(GameObject), false);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        parentTransform = (Transform)EditorGUILayout.ObjectField("Parent Transform", parentTransform, typeof(Transform), true);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate"))
        {
            Generate();
        }

        if (GUILayout.Button("Destroy"))
        {
            DestroyAll();
        }


        EditorGUILayout.EndHorizontal();
    }

    private void Generate()
    {
        for(int i=0 ; i<buttonsCount ; ++i)
        {
            string number = (i + 1).ToString();

            GameObject button = (GameObject)PrefabUtility.InstantiatePrefab(levelButtonPrefab, parentTransform);
            button.name = number;

            TMP_Text text = button.transform.GetChild(0).GetComponent<TMP_Text>();
            text.text = number;
        }
    }

    private void DestroyAll()
    {
        int c = parentTransform.childCount;
        for (int i = 0; i < c; ++i)
        {
            DestroyImmediate(parentTransform.GetChild(0).gameObject);
        }
    }
}