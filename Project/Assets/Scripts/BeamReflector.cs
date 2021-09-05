using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflector : BeamInteractor
{
    private void Awake()
    {
        SetStartPosRot(transform.parent ? transform.parent.transform : transform);
    }

    public override void OnBeamExit()
    {
        Debug.Log("Reflector Exit");
    }

    public override void OnBeamTrigger(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        RaycastHit rayHit;
        BeamInteractor beamInteractor;

        Vector3 incomingVec = hit.point - lastSentPos;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
        
        // Stack Overflow -- Risk -> reflect loop
        sender.FindInteractors(transform.position, reflectVec, out rayHit, out beamInteractor);
    }
}
