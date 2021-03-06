using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : CustomGameObject, IBeamInteractor
{
    public bool lightOn = false;
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    bool interacting = false;
    public bool Interacting { get => interacting; set => interacting = value; }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material = lightOn ? lightOnMaterial : lightOffMaterial;
    }

    public void OnBeamExit()
    {
        lightOn = false;
    }

    public void OnBeamEnter(RaycastHit hit, Vector3 lastSentPos)
    {
        lightOn = true;
    }

    public void OnBeamStay(RaycastHit hit,  Vector3 lastSentPos)
    {
        lightOn = true;
    }
}
