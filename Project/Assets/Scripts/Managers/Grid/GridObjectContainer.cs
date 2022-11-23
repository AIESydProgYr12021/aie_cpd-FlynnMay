using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridObjectContainer", menuName = "Grid/Container")]
public class GridObjectContainer : ScriptableObject
{
    static GridObjectContainer instance;
    [SerializeField] List<GridObject> gridObjects;
    [SerializeField] List<GridObject> gridProps;
    static GridObjectContainer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GridObjectContainer>("GridObjectContainer");

                for (int i = 0; i < instance.gridObjects.Count; i++)
                {
                    GridObject gridObject = instance.gridObjects[i];
                    gridObject.Index = i;
                }
                
                for (int i = 0; i < instance.gridProps.Count; i++)
                {
                    GridObject gridProp = instance.gridProps[i];
                    gridProp.Index = i;
                }
            }
            return instance;
        }
    }

    public static List<GridObject> GetGridObjects()
    {
        return Instance.gridObjects;
    }
    
    public static List<GridObject> GetGridProps()
    {
        return Instance.gridProps;
    }
}
