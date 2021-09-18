using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : CustomGameObject
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
        base.Start();
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
                return;
            }

            if (!hit.collider.CompareTag("GroundTile"))
                return;

            Vector3 pos = hit.collider.gameObject.transform.position;

            if (((int)pos.x != (int)transform.position.x && (int)pos.z != (int)transform.position.z))
                return;

            var dirToPos = (new Vector3(pos.x, transform.position.y, pos.z) - transform.position).normalized;

            dirToPos.x = Math.Abs(dirToPos.x);
            dirToPos.y = Math.Abs(dirToPos.y);
            dirToPos.z = Math.Abs(dirToPos.z);

            if (!(Vector3.Dot(new Vector3(1, transform.position.y, 0), dirToPos) >= 0.75f ||
                Vector3.Dot(new Vector3(0, transform.position.y, 1), dirToPos) >= 0.75f) && 
                !((pos.z == transform.position.z) && (pos.z == transform.position.z)))
            {
                return;
            }

            var checkVec = pos - transform.position;
            checkVec.y = 0.0f;

            var normalized = checkVec.normalized;
            //checkVec.Normalize();
            
            for (int i = 1; i <= Math.Abs(checkVec.x); i++)
            {
                RaycastHit checkHit;
                if (Physics.Raycast(new Vector3(transform.position.x + i * normalized.x, 1.0f, transform.position.z), Vector3.down, out checkHit))
                {
                    if (checkHit.collider.CompareTag("void"))
                    {
                        return;
                    }
                }
            }
            
            for (int i = 1; i <= Math.Abs(checkVec.z); i++)
            {
                RaycastHit checkHit;
                if (Physics.Raycast(new Vector3(transform.position.x, 1.0f, transform.position.z + i * normalized.z), Vector3.down, out checkHit))
                {
                    if (checkHit.collider.CompareTag("void"))
                    {
                        return;
                    }
                }
            }

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


