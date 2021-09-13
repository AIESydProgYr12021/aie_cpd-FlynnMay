using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PortalController : MonoBehaviour
{
    public List<Action> onPortalEnter = new List<Action>();
    public List<Action> onPortalStay = new List<Action>();
    public List<Action> onPortalExit = new List<Action>();

    private void OnTriggerEnter(Collider other)
    {
        foreach (var act in onPortalEnter)
        {
            act?.Invoke();
        }

        if (other.CompareTag("Player"))
        {
            GlobalControl.Instance.prevLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("LevelComplete");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (var act in onPortalStay)
        {
            act?.Invoke();
        }

        if (other.GetComponentInParent<MoveableObject>())
        {
            var customGameObject = other.gameObject.GetComponentInParent<CustomGameObject>();
            var rb = other.gameObject.GetComponentInParent<Rigidbody>();
            if (customGameObject && rb)
            {
                rb.gameObject.transform.position = customGameObject.startPos;
                rb.gameObject.transform.rotation = customGameObject.startRot;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }

            var lerptoVector = other.GetComponent<LerpToVector>();
            if (lerptoVector)
            {
                lerptoVector.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var act in onPortalExit)
        {
            act?.Invoke();
        }
    }
}
