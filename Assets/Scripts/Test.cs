using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap input;
    // Start is called before the first frame update
    public void Start()
    {
        InputReader reader = new InputReader(input);
        var grid = reader.ReadInputToGrid();
        ValuesManager<TileBase> valueManager = new ValuesManager<TileBase>(grid);
        PatternManager manager = new PatternManager(2);// this is where we change our pattern size
        manager.ProcessGrid(valueManager, false);
        WFCCore core = new WFCCore(5, 5, 500, manager);//this handels our demensions and iterations   (width, height, iterations)
        var result = core.CreateOutputGrid();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

