using Assets.Scripts.Interactors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableObject : CustomGameObject, IBeamInteractor, IPlayerInteractor
{
    LerpToVector lerpToVector;
    public Rigidbody rb;

    //bool interactingWithPlayer = false;

    //public bool InteractingWithPlayer { get => interactingWithPlayer; set => interactingWithPlayer = value; }

    private void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    public void OnBeamEnter(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeamExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeamStay(RaycastHit hit, BeamSpawner sender, Vector3 lastSentPos)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerEnter(GameObject player)
    {
        var agent = player.GetComponent<NavMeshAgent>();

        var pushDir = Vector3.zero;
        var dirFromPlayer = (transform.position - player.transform.position).normalized;

        float x = Mathf.Abs(dirFromPlayer.x);
        float z = Mathf.Abs(dirFromPlayer.z);

        if (x >= z)
            pushDir = new Vector3(dirFromPlayer.x, 0, 0);
        else
            pushDir = new Vector3(0, 0, dirFromPlayer.z);

        RaycastHit dropHit;
        if (Physics.Raycast(pushDir.normalized + rb.gameObject.transform.position, -rb.gameObject.transform.up, out dropHit))
        {
            Vector3 pos = dropHit.collider.gameObject.transform.position;
            LerpToVector lerpToVector = gameObject.GetComponent<LerpToVector>();

            if (dropHit.collider.gameObject.CompareTag("void"))
            {
                rb.useGravity = true;
                Vector3 objPos = rb.gameObject.transform.position;
                pos = objPos + pushDir;
                //return;
            }

            if (lerpToVector == null)
            {
                lerpToVector = gameObject.AddComponent<LerpToVector>();
            }

            lerpToVector.enabled = true;
            lerpToVector.targetPosition = new Vector3(pos.x, rb.gameObject.transform.position.y, pos.z);
            lerpToVector.lerpTime = 0.0f;
            lerpToVector.lerpSpeed = agent.speed + .1f;
            lerpToVector.OnTargetReached.Add(() =>
            {
                lerpToVector.enabled = false;
                Destroy(lerpToVector);
            });
        }
    }

    public void OnPlayerExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerStay()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerEnter()
    {
        throw new System.NotImplementedException();
    }
}
