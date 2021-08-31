using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflector : BeamInteractor
{
    BeamSpawner spawner;
    public override void OnBeamExit()
    {
        Debug.Log("Reflector Exit");
    }

    public override void OnBeamTrigger(RaycastHit hit, BeamSpawner sender)
    {
        spawner = sender;

        RaycastHit rayHit;
        BeamInteractor beamInteractor;


        Vector3 dir = Vector3.Reflect(-hit.transform.forward, hit.collider.transform.forward.normalized);

        // Stack Overflow
        sender.FindInteractors(transform.position, dir, out rayHit, out beamInteractor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner)
        {
            //spawner.foundInteractors
        }
    }
}
