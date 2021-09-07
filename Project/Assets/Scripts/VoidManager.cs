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
            rb.gameObject.transform.position = customGameObject.startPos;
            rb.gameObject.transform.rotation = customGameObject.startRot;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        var lerptoVector = other.GetComponent<LerpToVector>();
        if (lerptoVector)
        {
            lerptoVector.enabled = false;
        }
    }
}
