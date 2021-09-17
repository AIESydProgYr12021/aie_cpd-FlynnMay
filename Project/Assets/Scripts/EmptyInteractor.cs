using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyInteractor : CustomGameObject, IBeamInteractor
{
    bool interacting = false;
    public bool Interacting { get => interacting; set => interacting = value; }

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3.0f)
        {
            timer = 0.0f;
            Destroy(gameObject);
        }

    }

    public void OnBeamEnter(RaycastHit hit, Vector3 lastSentPos)
    {
    }

    public void OnBeamExit()
    {
        Destroy(gameObject);
    }

    public void OnBeamStay(RaycastHit hit, Vector3 lastSentPos)
    {

    }
}
