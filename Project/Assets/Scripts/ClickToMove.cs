using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject dropParticle;
    public GameObject hoverParticle;
    Vector3 destination;

    GameObject lastHovered;
    GameObject lastHoveredParticle;
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
            if (hit.collider.CompareTag("void"))
            {
                if (lastHovered)
                {
                    lastHovered = null;
                }

                if (lastHoveredParticle)
                {
                    Destroy(lastHoveredParticle);
                    lastHoveredParticle = null;
                }
            }

            if (!hit.collider.CompareTag("GroundTile"))
                return;

            Vector3 pos = hit.collider.gameObject.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                destination = new Vector3(pos.x, transform.position.y, pos.z);
                agent.SetDestination(destination);
                agent.velocity = Vector3.zero;
                if (dropParticle)
                {
                    var drop = Instantiate(dropParticle, null);
                    drop.transform.position = new Vector3(pos.x, 0.1f, pos.z);
                }
                agent.path.ClearCorners();
            }

            var hover = hit.collider.gameObject;
            if (hover)
            {
                if (hover != lastHovered)
                {
                    GameObject hoverParticleObj = null;
                    if (hoverParticle)
                    {
                        hoverParticleObj = Instantiate(this.hoverParticle, null);
                        hoverParticleObj.transform.position = new Vector3(pos.x, 0.1f, pos.z);
                    }

                    if (lastHovered)
                    {
                        lastHovered = null;
                    }

                    if (lastHoveredParticle)
                    {
                        Destroy(lastHoveredParticle);
                        lastHoveredParticle = null;
                    }

                    lastHovered = hover;
                    lastHoveredParticle = hoverParticleObj;
                }
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


