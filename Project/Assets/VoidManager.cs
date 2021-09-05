using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var customGameObject = other.gameObject.GetComponentInParent<CustomGameObject>();
        var rb = other.gameObject.GetComponentInParent<Rigidbody>();
        if (customGameObject && rb)
        {
            other.gameObject.transform.position = customGameObject.startPos;
            other.gameObject.transform.rotation = customGameObject.startRot;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
