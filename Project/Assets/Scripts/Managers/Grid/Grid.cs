using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
public struct Grid
{
    private Dictionary<int, Dictionary<int, int>> gridMap;
    [SerializeField] private int height;
    [SerializeField] int rows;
    [SerializeField] private int columns;
    private List<GridObject> gridObjects;
    public Dictionary<int, Dictionary<int, int>> GridMap { get => gridMap; set => gridMap = value; }
    public int Height { get => height; set => height = value; }
    public List<GridObject> GridObjects { get => gridObjects; set => gridObjects = value; }
    public int Rows { get => rows; }
    public int Columns { get => columns; }


    public Grid(int height = 0, int rows = 0, int columns = 0, List<GridObject> gridObjects = null, Dictionary<int, Dictionary<int, int>> map = null)
    {
        this.height = height;
        this.rows = rows;
        this.columns = columns;
        this.gridObjects = gridObjects;
        gridMap = (map ?? new Dictionary<int, Dictionary<int, int>>());
    }

    public void ClearGrid()
    {
        GridMap = new Dictionary<int, Dictionary<int, int>>();
        for (int z = 0; z < rows; z++)
        {
            GridMap[z] = new Dictionary<int, int>();
            for (int x = 0; x < columns; x++)
            {
                GridMap[z][x] = 0;
            }
        }
    }
}
