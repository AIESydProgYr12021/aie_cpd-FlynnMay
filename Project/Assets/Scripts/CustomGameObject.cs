using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGameObject : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;

    // Start is called before the first frame update
    public void SetStartPosRot(Transform tform)
    {
        startPos = tform.position;
        startRot = tform.rotation;
    }
}
