using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid floorGrid;
    Dictionary<int, Dictionary<int, int>> gridProps;
    int rows, cols;

    private void Awake()
    {
        SetGrid();
        LoadGrid(floorGrid);
    }

    private void SetGrid()
    {
        floorGrid = new Grid(0, 50, 50);
        floorGrid.ClearGrid();
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
            SetGrid();

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