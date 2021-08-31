using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBeam : MonoBehaviour
{
    [Header("Config")]
    public float rotSpeed;
    public Transform pivotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint.position, new Vector3(0, 0, 1), rotSpeed * Time.deltaTime);
    }
}
