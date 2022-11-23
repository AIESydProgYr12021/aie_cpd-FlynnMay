using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridObject", menuName = "Grid/Object")]
public class GridObject : ScriptableObject
{
    public GameObject prefab;
    public int Index { get; set; } = 0;
}
