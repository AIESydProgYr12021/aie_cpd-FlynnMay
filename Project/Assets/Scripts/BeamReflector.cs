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

        Vector3 incomingVec = hit.point - sender.transform.position;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
        
        // Stack Overflow -- Risk -> reflect loop
        sender.FindInteractors(transform.position, reflectVec, out rayHit, out beamInteractor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
