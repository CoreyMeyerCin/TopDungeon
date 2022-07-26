using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

namespace WaveFunctionCollapse {
    public class PatternDataResults
    {
        private int[][] patternIndiciesGrid;
        public Dictionary<int, PatternData> PatternIndexDictionary { get; private set; }

        public PatternDataResults(int[][] patternIndiciesGrid, Dictionary<int, PatternData> patternIndexDictionary)
        {
            this.patternIndiciesGrid = patternIndiciesGrid;
            PatternIndexDictionary = patternIndexDictionary;
        }

        public int GetGridLengthX()
        {
            return patternIndiciesGrid[0].Length;
        }

        public int GetGridLengthY()
        {
            return patternIndiciesGrid.Length;
        }

        public int GetIndexAt(int x, int y)
        {
            return patternIndiciesGrid[y][x];
        }

        public int GetNeighbourInDirection(int x, int y, Direction dir)
        {
            if(patternIndiciesGrid.CheckJaggedArray2IfIndexIsValid(x,y) == false)
            {
                return -1;
            }
            switch (dir)
            {
                case Direction.Up:
                    if (patternIndiciesGrid.CheckJaggedArray2IfIndexIsValid(x, y + 1))
                    {
                        return GetIndexAt(x, y+1);
                    }
                    return -1;
                case Direction.Down:
                    if (patternIndiciesGrid.CheckJaggedArray2IfIndexIsValid(x, y - 1))
                    {
                        return GetIndexAt(x, y - 1);
                    }
                    return - 1;
                case Direction.Left:
                    if (patternIndiciesGrid.CheckJaggedArray2IfIndexIsValid(x-1, y))
                    {
                        return GetIndexAt(x-1, y);
                    }
                    return - 1;
                case Direction.Right:
                    if (patternIndiciesGrid.CheckJaggedArray2IfIndexIsValid(x + 1, y))
                    {
                        return GetIndexAt(x + 1, y);
                    }
                    return -1;
                default:
                    return -1;
            }
        }
    }

}