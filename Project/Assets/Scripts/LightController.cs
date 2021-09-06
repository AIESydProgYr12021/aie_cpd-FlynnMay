using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : CustomGameObject, IBeamInteractor
{
    public bool lightOn = false;
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material = lightOn ? lightOnMaterial : lightOffMaterial;
    }

    public void OnBeamExit()
    {
        lightOn = false;
    }

    public void OnBeamEnter(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        lightOn = true;
    }

    public void OnBeamStay(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        lightOn = true;
    }
}
