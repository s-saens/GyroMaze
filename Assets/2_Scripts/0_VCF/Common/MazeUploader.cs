using UnityEngine;
using Firebase.Database;

public class MazeUploader
{
    public void Upload(int index, Maze maze)
    {
        DatabaseReference r = DBRef.maze.Child(index.ToString());
        r.SetValueAsync(maze);
    }
}