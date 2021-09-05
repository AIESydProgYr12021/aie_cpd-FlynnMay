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
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (var act in onPortalStay)
        {
            act?.Invoke();
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
