using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap input;

    public void Start()
    {
        InputReader reader = new InputReader(input);
        var grid = reader.ReadInputToGrid();
        for(int row=0; row<grid.Length; row++)
        {
            for(int col = 0; col < grid[0].Length; col++)
            {
                Debug.Log("Row:" + row + " Col:" + col + " Tile name " + grid[row][col].Value.name);
            }
        }
    }
}
