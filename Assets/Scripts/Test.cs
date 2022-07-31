using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap inputTilemap;
    public Tilemap outputTilemap;

    public int patternSize;
    public int maxIteration = 500;
    public int outputWidth = 5;
    public int outputHeight = 5;
    public bool equalWeights = false;

    ValuesManager<TileBase> valueManager;
    WFCCore core;
    PatternManager manager;
    public TileMapOutput output;

    // Start is called before the first frame update
    public void Start()
    {
        CreateWFC();

    }

    public void CreateWFC()
    {
        InputReader reader = new InputReader(inputTilemap);
        var grid = reader.ReadInputToGrid();
        valueManager = new ValuesManager<TileBase>(grid);
        manager = new PatternManager(patternSize);// this is where we change our pattern size
        manager.ProcessGrid(valueManager, equalWeights);
        core = new WFCCore(outputWidth, outputHeight, maxIteration, manager);//this handels our demensions and iterations   (width, height, iterations)

        

    }

    public void CreateTilemap()
    {
        //Debug.Log("Inside of CreateTilemap()");
        output = new TileMapOutput(valueManager, outputTilemap);
        var result = core.CreateOutputGrid();
        output.CreateOutput(manager, result,outputWidth,outputHeight);
    }

    public void SaveTilemap()
    {
        if (output.OutputImage != null)
        {
            outputTilemap = output.OutputImage;
            GameObject objectToSave = outputTilemap.gameObject;

            PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Saved/output.prefab");
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}

