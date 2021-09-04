using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeamInteractor : MonoBehaviour
{
    public abstract void OnBeamTrigger(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos);

    public abstract void OnBeamExit();
}
