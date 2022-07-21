using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{
    public MazeFactory factory;
    public MazeGenerator generator = new MazeGenerator();
    public int sizeX = 10;
    public int sizeY = 10;

    void Start()
    {
        factory.MakeMaze(generator.MakeMazeDFS(sizeX, sizeY, 0, 0));
    }
}
