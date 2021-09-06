using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : CustomGameObject
{
    public float spawnerDelay = 0.5f;
    public float timer = 0;
    public GameObject beam;

    public List<CustomGameObject> foundInteractors = new List<CustomGameObject>();

    // Start is called before the first frame update
    private void Awake()
    {
        SetStartPosRot(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
            return;

        RaycastHit hit;
        IBeamInteractor beamCollider;

        FindInteractors(transform.position, transform.forward, out hit, out beamCollider);

        if (beamCollider == null)
            return;


        timer += Time.deltaTime;

        if (timer >= spawnerDelay)
        {
            timer = 0;
            var beamObj = Instantiate(beam);

            LerpToVector lerpToVector = beamObj.GetComponent<LerpToVector>();
            FollowPath followPath = beamObj.GetComponent<FollowPath>();

            foreach (var interactor in foundInteractors)
            {
                followPath.path.Add(interactor.gameObject.transform.position);
            }

            beamObj.transform.position = transform.position;
            lerpToVector.currentPosition = beamObj.transform.position;
            beamObj.transform.forward = transform.forward;

            lerpToVector.targetPosition = hit.collider.gameObject.transform.position;
            lerpToVector.lerpTime = 0.0f;
        }
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

            if (beamInteractor != null)
            {
                if (!foundInteractors.Contains(cObj))
                    foundInteractors.Add(cObj);

                beamInteractor.OnBeamStay(hit, this, pos);
            }
            else
            {
                RunExit();
            }
        }
        else
        {
            RunExit();
        }

    }

    private void RunExit()
    {
        foreach (var interactor in foundInteractors)
        {
            interactor.GetComponent<IBeamInteractor>().OnBeamExit();
        }

        foundInteractors.Clear();
    }
}