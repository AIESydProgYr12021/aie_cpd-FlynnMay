using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflector : CustomGameObject, IBeamInteractor, IBeamSender
{
    public GameObject emptyOBJ;

    List<IBeamSender> visited = new List<IBeamSender>();
    List<CustomGameObject> found = new List<CustomGameObject>();

    public List<CustomGameObject> foundInteractors = new List<CustomGameObject>();
    public List<IBeamSender> Visited { get => visited; set => visited = value; }
    public List<CustomGameObject> Found { get => found; set => found = value; }

    bool interacting = false;
    public bool Interacting { get => interacting; set => interacting = value; }

    private void Awake()
    {
        SetStartPosRot(transform.parent ? transform.parent.transform : transform);
    }

    public void OnBeamExit()
    {
        Debug.Log("Reflector Exited Beam");
    }

    public void OnBeamEnter(RaycastHit hit, Vector3 lastSentPos)
    {
        OnBeamStay(hit, lastSentPos);
    }

    public List<CustomGameObject> SendBeam(Vector3 pos, Vector3 dir)
    {
        found.Clear();
        if (visited.Contains(this))
            return found;

        visited.Add(this);
        RaycastHit hit;
        Debug.DrawRay(pos, dir * 50);
        if (Physics.Raycast(pos, dir, out hit))
        {
            //found.Add(this);
            var other = hit.collider.gameObject;
            var otherCustom = other.GetComponentInParent<CustomGameObject>();
            var otherInteractor = other.GetComponent<IBeamInteractor>();
            var otherSender = other.GetComponent<IBeamSender>();
            if (otherInteractor != null)
            {
                if (!otherInteractor.Interacting)
                {
                    otherInteractor.OnBeamEnter(hit, pos);
                    otherInteractor.Interacting = true;
                }
                else
                {
                    otherInteractor.OnBeamStay(hit, pos);
                }
                found.Add(otherCustom);
            }

            if (otherCustom != null)
            {
                found.Add(otherCustom);
            }

            if (otherSender != null)
            {
                foreach (var cObj in otherSender.Found)
                {
                    if (!found.Contains(cObj))
                    {
                        found.Add(cObj);
                    }
                }
            }
        }
        visited.Clear();
        return found;
    }

    public void OnBeamStay(RaycastHit hit, Vector3 lastSentPos)
    {
        Vector3 incomingVec = hit.point - lastSentPos;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

        SendBeam(transform.position, reflectVec);

        if (found.Count <= 0)
        {
            if (emptyOBJ)
            {
                var eCObj = Instantiate(emptyOBJ);
                eCObj.transform.position = transform.position + (reflectVec.normalized * 4);
                var interactor = eCObj.GetComponentInChildren<IBeamInteractor>();
                interactor.Interacting = true;
                found.Add(eCObj.GetComponent<CustomGameObject>());
            }
        }
    }
}
