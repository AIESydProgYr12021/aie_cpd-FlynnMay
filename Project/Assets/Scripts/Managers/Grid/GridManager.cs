using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GridManager : MonoBehaviour
{
    [SerializeField] int rows, cols;
    Grid floorGrid;
    Grid propGrid;

    private void Awake()
    {
        BuildGrid();
        LoadGrid();
    }

    private void BuildGrid()
    {
        Map map = Map.Deserialize(0);
        floorGrid = new Grid(0, GridObjectContainer.GetGridObjects(), map.floor);
        propGrid = new Grid(1, rows, cols, GridObjectContainer.GetGridProps());
        propGrid.ClearGrid();
    }

    private void LoadGrid()
    {
        LoadGrid(floorGrid);
        LoadGrid(propGrid);
    }

    private void LoadGrid(Grid grid)
    {
        var pos = Vector3.zero;
        List<GridObject> objects = grid.GridObjects;
        pos.y = grid.Height;

        for (int z = 0; z < grid.Rows; z++)
        {
            for (int x = 0; x < grid.Columns; x++)
            {
                pos.x = x;
                pos.z = z;

                int index = grid.GridMap[z][x] - 1;
                if (index < 0)
                    continue;

                GameObject gridObject = Instantiate(objects[index].prefab);
                gridObject.transform.position = pos;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (floorGrid.GridMap == null)
            BuildGrid();

        var pos = Vector3.zero;
        pos.y = floorGrid.Height;
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < cols; x++)
            {
                pos.x = x;
                pos.z = z;

                Gizmos.color = floorGrid.GridMap[z][x] == 0 ? Color.blue : Color.red;
                Gizmos.DrawWireCube(pos, new Vector3(1 - 0.05f, 0.01f, 1 - 0.05f));
            }
        }
    }
}

[Serializable]
public class Map
{
    public int a = 0;
    public int[,] floor;
    public int[,] props;

    public Map() { }

    public Map(int row, int col)
    {
        floor = new int[row, col];
        props = new int[row, col];
    }

    public static void Serialize(Map map, int levelNumber)
    {
        string path = $"{Application.streamingAssetsPath}/{levelNumber}.json";
        JsonHelpers.WriteToJsonFile(path, map);
    }

    public static Map Deserialize(int levelNumber)
    {
        string path = $"{Application.streamingAssetsPath}/{levelNumber}.json";
        return JsonHelpers.ReadFromJsonFile<Map>(path);
    }
}