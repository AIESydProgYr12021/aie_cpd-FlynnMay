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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("IgnoreCamRaycast");
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Outline outline = hit.collider.GetComponent<Outline>();
            //outline.enabled = true;
            if (!hit.collider.CompareTag("GroundTile"))
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = hit.collider.gameObject.transform.position;
                agent.SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
                if (dropParticle)
                {
                    var drop = Instantiate(dropParticle, null);
                    drop.transform.position = new Vector3(pos.x, 0.1f, pos.z);
                }
                agent.path.ClearCorners();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        MoveableObject moveableObject = other.gameObject.GetComponentInParent<MoveableObject>();

        if (moveableObject)
        {
            moveableObject.OnPlayerEnter(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MoveableObject moveableObject = other.gameObject.GetComponentInParent<MoveableObject>();

        if (moveableObject)
        {
            moveableObject.OnPlayerStay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MoveableObject moveableObject = other.gameObject.GetComponentInParent<MoveableObject>();

        if (moveableObject)
        {
            moveableObject.OnPlayerExit();
        }
    }

}


