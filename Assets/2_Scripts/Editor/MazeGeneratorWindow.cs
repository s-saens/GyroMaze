#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;

public class MazeGeneratorWindow : EditorWindow
{
    private Vector2Int size;
    private Vector2Int range; // both inclusive
    private Maze[] mazes;
    
    private MazeGenerator mg = new MazeGenerator();
    private MazeUploader mu = new MazeUploader();

    // Add menu item named "My Window" to the Window menu
    [MenuItem("SAENS/MazeGeneratorWindow")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MazeGeneratorWindow));
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        size = EditorGUILayout.Vector2IntField("Maze Size", size);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        range = EditorGUILayout.Vector2IntField("range", range);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate")) GenerateMaze();
        if (GUILayout.Button("Upload")) UploadMaze();

        EditorGUILayout.EndHorizontal();
    }

    private void GenerateMaze()
    {
        int count = range.x - range.y + 1;

        mazes = new Maze[count];
        for (int i = 0; i < count; ++i)
        {
            mazes[i] = mg.MakeMazeDFS(size.x, size.y, size.x/2, size.y/2);
        }
    }

    private void UploadMaze()
    {
        for(int i=range.x ; i<=range.y ; ++i)
        {
            mu.Upload(i, mazes[i]);
        }
    }
}
#endif