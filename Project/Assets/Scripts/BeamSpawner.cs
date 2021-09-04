using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    public float spawnerDelay = 0.5f;
    public float timer = 0;
    public GameObject beam;

    public List<BeamInteractor> foundInteractors = new List<BeamInteractor>();

    // Start is called before the first frame update
    void Start()
    {
        //beam.GetComponent<LerpToVector>().target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
            return;

        RaycastHit hit;
        BeamInteractor beamCollider;

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

            //List<Vector3> path = new List<Vector3>();

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

    public void FindInteractors(Vector3 pos, Vector3 dir, out RaycastHit hit, out BeamInteractor beamCollider)
    {
        beamCollider = null;

        Debug.DrawRay(pos, dir, Color.green);
        if (Physics.Raycast(pos, dir, out hit))
        {
            beamCollider = hit.collider.gameObject.GetComponent<BeamInteractor>();

            if (beamCollider)
            {
                if (!foundInteractors.Contains(beamCollider))
                    foundInteractors.Add(beamCollider);

                beamCollider.OnBeamTrigger(hit, this, pos);
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
            interactor.OnBeamExit();
        }

        foundInteractors.Clear();
    }
}
