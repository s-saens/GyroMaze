using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;

public class MazeUploader
{
    public void Upload(int index, Maze maze)
    {
        DatabaseReference r = FirebaseDBReference.Reference("maze", index.ToString());
        r.SetRawJsonValueAsync(JsonConvert.SerializeObject(maze));
    }
}