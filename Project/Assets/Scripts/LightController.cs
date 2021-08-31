using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : BeamInteractor
{
    public bool lightOn = false;
    public Material lightOnMaterial;
    public Material lightOffMaterial;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lightOn = false;

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit))
        //{
        //    var spawner = hit.collider.gameObject.GetComponent<BeamSpawner>();

        //    if (spawner)
        //    {
        //        lightOn = true;
        //    }
        //}

        GetComponent<Renderer>().material = lightOn ? lightOnMaterial : lightOffMaterial;
    }
    public override void OnBeamTrigger(RaycastHit hit, BeamSpawner sender)
    {
        lightOn = true;
    }

    public override void OnBeamExit()
    {
        lightOn = false;
    }
}
