#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

public class MazeGeneratorWindow : EditorWindow
{
    private Vector2Int size;
    private int count;
    
    private MazeGenerator mg = new MazeGenerator();

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

        count = EditorGUILayout.IntField("Count to make", count);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate"))
        {
            Maze[] mazes = new Maze[count];
            for(int i=0 ; i<count ; ++i)
            {
                mazes[i] = mg.MakeMazeDFS(size.x, size.y, 0, 0);
            }
            string json = JsonConvert.SerializeObject(mazes);
            Debug.Log(json);
        }

        EditorGUILayout.EndHorizontal();
    }
}
#endif