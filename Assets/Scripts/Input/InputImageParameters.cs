using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse { 
public class InputImageParameters
{
        Vector2Int? bottonRightTileCoords = null;
        Vector2Int? topLeftTileCoords = null;
        BoundsInt inputTileMapBounds;
        TileBase[] inputTilemapsTileArray;
        Queue<TileContainer> stackOfTiles = new Queue<TileContainer>();
        private int width = 0, height = 0;
        private Tilemap inputTilemap;

        public Queue<TileContainer> StackOfTiles { get => stackOfTiles; set => stackOfTiles = value; }
        public int Width { get => width;}
        public int Height { get => height;}

        public InputImageParameters(Tilemap inputTilemap)
        {
            this.inputTilemap = inputTilemap;
            this.inputTileMapBounds = this.inputTilemap.cellBounds;
            this.inputTilemapsTileArray = this.inputTilemap.GetTilesBlock(this.inputTileMapBounds);
            ExtractNonEmptyTiles();
            VeryfyInputTiles();
        }

        private void VeryfyInputTiles()
        {
            if(topLeftTileCoords == null || bottonRightTileCoords == null)
            {
                throw new System.Exception("WFC: Input Tilemap is empty");
            }
            int minX = bottonRightTileCoords.Value.x;
            int maxX = topLeftTileCoords.Value.x;
            int minY = bottonRightTileCoords.Value.y;
            int maxY = topLeftTileCoords.Value.y;

            width = Math.Abs(maxX - minX) + 1;
            height = Math.Abs(maxY - minY) + 1;

            int tileCount = width * height;
            if(StackOfTiles.Count != tileCount)
            {
                throw new System.Exception("WFC: Tilemap has empty fields");
            }
            if(stackOfTiles.Any(tile => tile.X > maxX || tile.X < minX || tile.Y > maxY || tile.Y < minY))
            {
                throw new System.Exception("WFC: Tilemap image should be a filled rectangle");
            } 
        }


        /*
         This next method will change a 1D array into a 2D array based on the width(which is inputTileMapBounds.size.x)

        with array [a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y] with a width of 5 will output the following:

        [   a,b,c,d,e,
            f,g,h,i,j,
            k,l,m,n,o,
            p,q,r,s,t,
            u,v,w,x,y]
         */

        private void ExtractNonEmptyTiles()
        {
            for(int row = 0; row < this.inputTileMapBounds.size.y; row++)
            {
                for(int col=0; col < this.inputTileMapBounds.size.x; col++)
                {
                    int index = col + (row * inputTileMapBounds.size.x);
                    TileBase tile = inputTilemapsTileArray[index];
                    if(bottonRightTileCoords==null && tile!= null)
                    {
                        bottonRightTileCoords = new Vector2Int(col, row);
                    }
                    if(tile != null)
                    {
                        StackOfTiles.Enqueue(new TileContainer(tile, col,row));
                        topLeftTileCoords = new Vector2Int(col, row);
                    }
                }
            }
        }
    }

}
