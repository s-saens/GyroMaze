using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;

public class MazeUploader
{
    public void Upload(int index, Maze maze)
    {
        FirebaseDBAccessor.SetValue<Maze>(
            FirebaseDBReference.Reference("maze", index.ToString()),
            maze
        );
    }
}