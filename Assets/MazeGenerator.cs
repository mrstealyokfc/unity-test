using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class MazeGenerator : MonoBehaviour
{

    public GameObject wall;

    public int width, height, exits;
    // Start is called before the first frame update
    void Start()
    {
        cells = new bool[width, height];
        horizontalWalls = new bool[width+1, height+1];
        veritcalWalls = new bool[width+1, height+1];
        CreateMaze();
    }
    
    bool[,] horizontalWalls;
    bool[,] veritcalWalls;
    bool[,] cells;

    Random _rng = new Random();
    (int x, int y) cur;

    void CreateMaze()
    {
        GenerateMaze();
        GenerateExits();
        LoadMazeIntoGame();
    }

    void GenerateExits()
    {
        
    }

    void LoadMazeIntoGame()
    {
        float size = wall.GetComponent<Renderer>().bounds.size.x;
        for(int y=0;y<height+1;y++){ 
            for (int x = 0; x < width+1; x++)
            { 
                Vector3 xoffSet = new Vector3((x * size), 0, (y * size)-size/2); 
                Vector3 yoffSet = new Vector3((x * size)-size/2, 0, (y * size)); 
                if (!veritcalWalls[x, y] && x < width) 
                    Instantiate(wall, gameObject.transform.position + xoffSet,
                        Quaternion.Euler(0,0,0),gameObject.transform);
                if (!horizontalWalls[x, y] && y < height)
                    Instantiate(wall, gameObject.transform.position + yoffSet,
                        Quaternion.Euler(0,-90,0),gameObject.transform);
            }
        }
    }
        
    private void GenerateMaze()
    { 
        //implmenttation of reverse thing algotheingy
        Stack<(int, int)> cellTrail = new Stack<(int, int)>();
        cur = (_rng.Next(0, width - 1), _rng.Next(0, height - 1));

        do {
            cells[cur.x, cur.y] = true; 
            if (GetAdjCells().Length == 0)
                cur = cellTrail.Pop();
            else
            { var next = GetAdjCells()[_rng.Next(0, GetAdjCells().Length)];
                int dx = next.Item1 - cur.x;
                int dy = next.Item2 - cur.y; 
                Console.WriteLine(cur+" | "+next);
                if (dx > 0)
                    veritcalWalls[cur.y, (cur.x + 1)] = true;
                if (dy > 0)
                    horizontalWalls[(cur.y + 1), cur.x] = true;
                if (dx < 0)
                    veritcalWalls[cur.y, cur.x ] = true;
                if (dy < 0)
                    horizontalWalls[cur.y , cur.x] = true;
                cellTrail.Push(cur);
                cur = next;
            }
        } while (cellTrail.Count > 0);
    }

    private (int, int)[] GetAdjCells()
    { 
        List<(int, int)> adjCells = new List<(int, int)>();
        
        (int x, int y)[] offsets =
        { 
            (0, 1),
            (1, 0),
            (-1, 0),
            (0, -1)
        };
        foreach((int x,int y) set in offsets)
        {
            try
            {
                if(!cells[cur.x+set.x,cur.y+set.y])
                    adjCells.Add((cur.x+set.x,cur.y+set.y));
            }
            catch (IndexOutOfRangeException e) { }
        }
        return adjCells.ToArray();
    }
}
