using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflector : CustomGameObject, IBeamInteractor
{
    bool interacting = false;
    public bool Interacting { get => interacting; set => interacting = value; }

    private void Awake()
    {
        SetStartPosRot(transform.parent ? transform.parent.transform : transform);
    }

    public void OnBeamExit()
    {
        //Debug.Log("Reflector Exit");
    }

    public void OnBeamEnter(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {

    }

    public void OnBeamStay(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        RaycastHit rayHit;
        IBeamInteractor beamInteractor;

        Vector3 incomingVec = hit.point - lastSentPos;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

        // Stack Overflow -- Risk -> reflect loop
        sender.FindInteractors(transform.position, reflectVec, out rayHit, out beamInteractor);
    }
}
