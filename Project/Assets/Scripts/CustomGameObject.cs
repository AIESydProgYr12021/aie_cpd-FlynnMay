using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGameObject : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    Rigidbody rb;

    // Start is called before the first frame update
    public void SetStartPosRot(Transform tform)
    {
        startPos = tform.position;
        startRot = tform.rotation;
    }

    public void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    
    // Object Drop Fix, No work-o
    //public void LateUpdate()
    //{
    //    if (rb == null)
    //        return;

    //    if (Physics.Raycast(rb.transform.position, -rb.transform.up, out RaycastHit hit))
    //    {
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            rb.transform.position = new Vector3(rb.transform.position.x, startPos.y, rb.transform.position.z);
    //            Debug.Log("Player Detected, Can't Drop Obj");
    //        }
    //    }
    //}
}
