using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject dropParticle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 pos = hit.collider.gameObject.transform.position;
                agent.SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
                if (dropParticle)
                {
                    var drop = Instantiate(dropParticle, null);
                    drop.transform.position = new Vector3 (pos.x, 0.1f, pos.z);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("movable"))
            return;

        var rb = collision.gameObject.GetComponentInParent<Rigidbody>();
        if (rb == null)
            return;

        var pushDir = Vector3.zero;

        float x = Mathf.Abs(transform.forward.x);
        float z = Mathf.Abs(transform.forward.z);

        if (x >= z)
            pushDir = new Vector3(transform.forward.x, 0, 0);
        else
            pushDir = new Vector3(0, 0, transform.forward.z);

        RaycastHit dropHit;
        if (Physics.Raycast(pushDir.normalized + rb.gameObject.transform.position, -rb.gameObject.transform.up, out dropHit))
        {
            Vector3 pos = dropHit.collider.gameObject.transform.position;
            LerpToVector lerpToVector = rb.gameObject.GetComponent<LerpToVector>();

            if (dropHit.collider.gameObject.CompareTag("void"))
            {
                rb.useGravity = true;
                Vector3 objPos = rb.gameObject.transform.position;
                pos = objPos + pushDir;
                //return;
            }

            if (lerpToVector == null)
            {
                lerpToVector = rb.gameObject.AddComponent<LerpToVector>();
            }

            lerpToVector.enabled = true;
            lerpToVector.targetPosition = new Vector3(pos.x, rb.gameObject.transform.position.y, pos.z);
            lerpToVector.lerpTime = 0.0f;
            lerpToVector.lerpSpeed = agent.speed + .1f;
            lerpToVector.OnTargetReached.Add(() =>
            {
                lerpToVector.enabled = false;
            });
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
    }
}


