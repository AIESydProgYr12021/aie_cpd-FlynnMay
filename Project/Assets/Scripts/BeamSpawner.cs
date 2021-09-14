using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : CustomGameObject, IBeamSender
{
    public float spawnerDelay = 0.5f;
    public float timer = 0;
    public GameObject beam;
    public GameObject emptyOBJ;

    List<IBeamSender> visited = new List<IBeamSender>();
    List<CustomGameObject> found = new List<CustomGameObject>();

    public List<CustomGameObject> lastFound = new List<CustomGameObject>();
    public List<IBeamSender> Visited { get => visited; set => visited = value; }
    public List<CustomGameObject> Found { get => found; set => found = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        SetStartPosRot(transform);
    }

    // Update is called once per frame
    void Update()
    {
        SendBeam(transform.position, transform.forward);

        RunExit();

        timer += Time.deltaTime;

        if (timer >= spawnerDelay)
        {
            timer = 0;
            var beamObj = Instantiate(beam);

            LerpToVector lerpToVector = beamObj.GetComponent<LerpToVector>();
            FollowPath followPath = beamObj.GetComponent<FollowPath>();

            if (found.Count <= 0)
            {
                followPath.path.Add(transform.position + (transform.forward * 6));
            }

            foreach (var interactor in found)
            {
                followPath.path.Add(interactor.gameObject.transform.position);
            }
            followPath.OnPathEnd.Add(() => Destroy(beamObj));
            beamObj.transform.position = transform.position;
            lerpToVector.currentPosition = beamObj.transform.position;
            beamObj.transform.forward = transform.forward;

            lerpToVector.targetPosition = followPath.path[0];
            lerpToVector.lerpTime = 0.0f;
        }
        lastFound.Clear();
        lastFound.AddRange(found);
    }

    public void FindInteractors(Vector3 pos, Vector3 dir, out RaycastHit hit, out IBeamInteractor beamInteractor)
    {
        beamInteractor = null;

        Debug.DrawRay(pos, dir, Color.green);
        if (Physics.Raycast(pos, dir, out hit))
        {
            var obj = hit.collider.gameObject;
            beamInteractor = obj.GetComponent<IBeamInteractor>();
            CustomGameObject cObj = obj.GetComponent<CustomGameObject>();

            if (cObj == null)
                return;

            if (beamInteractor != null)
            {
                if (!lastFound.Contains(cObj))
                    lastFound.Add(cObj);

                if (!beamInteractor.Interacting)
                {
                    beamInteractor.OnBeamEnter(hit, pos);
                    beamInteractor.Interacting = true;
                }
                else
                {
                    beamInteractor.OnBeamStay(hit, pos);
                }
            }
            else
            {
                RunExit();
            }
        }
    }

    private void RunExit()
    {
        foreach (var interactorObj in lastFound)
        {
            if (found.Contains(interactorObj))
                continue;

            IBeamInteractor interactor = interactorObj.GetComponent<IBeamInteractor>();
            if (interactor != null)
            {
                if (interactor.Interacting)
                {
                    interactor.OnBeamExit();
                    interactor.Interacting = false;
                }
            }
        }
    }

    public List<CustomGameObject> SendBeam(Vector3 pos, Vector3 dir)
    {
        found.Clear();

        if (visited.Contains(this))
            return found;

        visited.Add(this);
        RaycastHit hit;
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
            else if (otherCustom != null)
            {
                found.Add(otherCustom);
            }
            else if (otherSender != null)
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
}
