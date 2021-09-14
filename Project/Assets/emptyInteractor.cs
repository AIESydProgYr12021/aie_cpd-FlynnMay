using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyInteractor : CustomGameObject, IBeamInteractor
{
    bool interacting = false;
    public bool Interacting { get => interacting; set => interacting = value; }

    public void OnBeamEnter(RaycastHit hit, Vector3 lastSentPos)
    {
        Destroy(gameObject);

    }

    public void OnBeamExit()
    {
        Destroy(gameObject);
    }

    public void OnBeamStay(RaycastHit hit, Vector3 lastSentPos)
    {
        Destroy(gameObject);

    }
}
