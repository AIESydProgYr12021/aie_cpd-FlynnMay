using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightManager : MonoBehaviour
{
    public List<LightController> lights = new List<LightController>();
    public List<BeamSpawner> spawners = new List<BeamSpawner>();
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        foreach (var light in lights)
        {
            light.lightOn = false;
        }

        //foreach (var spawner in spawners)
        //{
        //    spawner
        //}
    }
}
