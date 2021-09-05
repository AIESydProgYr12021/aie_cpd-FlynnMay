using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LightManager : MonoBehaviour
{
    public List<LightController> lights = new List<LightController>();
    public List<Action> onGameWon = new List<Action>();
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        lights.AddRange(GetComponentsInChildren<LightController>());
        portal.SetActive(false);
    }

    void Update()
    {
        bool gameWon = true;
        foreach (var light in lights)
        {
            if (!light.lightOn)
            {
                gameWon = false;
                break;
            }
        }

        if (gameWon)
        {
            foreach (var act in onGameWon)
            {
                act?.Invoke();
            }
            portal.SetActive(true);
        }

        //foreach (var spawner in spawners)
        //{
        //    spawner
        //}
    }
}
