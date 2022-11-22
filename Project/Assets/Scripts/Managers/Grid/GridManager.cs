using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Dictionary<int, Dictionary<int, int>> gridFloor;
    Dictionary<int, Dictionary<int, int>> gridProps;
    int rows, cols;

    private void Awake()
    {
        SetGrid();
        LoadGrid();
    }

    private void SetGrid()
    {
        gridFloor = new Dictionary<int, Dictionary<int, int>>();
        gridProps = new Dictionary<int, Dictionary<int, int>>();
        rows = 50;
        cols = 50;
        for (int z = 0; z < rows; z++)
        {
            gridFloor[z] = new Dictionary<int, int>();
            for (int x = 0; x < cols; x++)
            {
                gridFloor[z][x] = Random.Range(0, 2);
                gridProps[z][x] = Random.Range(0, 2);
            }
        }
    }

    private void LoadGrid()
    {
        var pos = Vector3.zero;
        List<GridObject> objects = GridObjectContainer.GetGridObjects();

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < cols; x++)
            {
                pos.x = x;
                pos.z = z;

                int index = gridFloor[z][x] - 1;
                if (index < 0)
                    continue;

                GameObject gridObject = Instantiate(objects[index].prefab);
                gridObject.transform.position = pos;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gridFloor == null)
            SetGrid();

        var pos = Vector3.zero;

        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < cols; x++)
            {
                pos.x = x;
                pos.z = z;

                Gizmos.color = gridFloor[z][x] == 0 ? Color.blue : Color.red;
                Gizmos.DrawWireCube(pos, new Vector3(1 - 0.05f, 0.01f, 1 - 0.05f));
            }
        }
    }
}