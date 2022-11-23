using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int rows, cols;
    Grid floorGrid;
    Grid propGrid;

    private void Awake()
    {
        Map map = new Map(10, 10);
        Map.Serialize(map, 0);
        BuildGrid();
        LoadGrid();
    }

    private void BuildGrid()
    {
        floorGrid = new Grid(0, rows, cols, GridObjectContainer.GetGridObjects());
        propGrid = new Grid(1, rows, cols, GridObjectContainer.GetGridProps());
        floorGrid.ClearGrid();
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

                int index = grid.GridMap[z][x];
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
    public int[][] grid;
    public Map(int row, int col)
    {
        grid = new int[row][];
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            grid[y] = new int[col];
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                grid[y][x] = 0;
            }
        }
    }

    public static void Serialize(Map map, int levelNumber)
    {
        string json = JsonUtility.ToJson(map, true);
        string path = $"{Application.streamingAssetsPath}/{levelNumber}.json";

        using (StreamWriter streamWriter = File.CreateText(path))
            streamWriter.Write(json);
    }
}