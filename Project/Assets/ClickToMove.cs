using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;

    Dictionary<GameObject, Quaternion> objectRotationPair = new Dictionary<GameObject, Quaternion>();
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
            }
        }

        //if (!agent.hasPath)
        //    return;

        //if (Physics.Raycast(headTransform.position, headTransform.forward, out hit, 1))
        //{
        //    if (hit.collider.gameObject.CompareTag("movable"))
        //    {

        //        var dirToTarget = (agent.pathEndPosition - transform.position).normalized;

        //        if (Vector3.Dot(headTransform.forward, dirToTarget) >= 1)
        //        {
        //            hit.collider.gameObject.transform.SetParent(headTransform);

        //        }

        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.gameObject.GetComponentInParent<Rigidbody>();
        if (rb == null)
            return;

        var pushDir = Vector3.zero;

        float x = Mathf.Abs(transform.forward.x);
        float z = Mathf.Abs(transform.forward.z);
        if (x >= z)
            pushDir = new Vector3(transform.forward.x, 0, 0);
        else
            pushDir = new Vector3(0, 0, transform.forward.z);

        rb.gameObject.GetComponentInParent<Rigidbody>().velocity = pushDir * agent.speed;

        objectRotationPair.Add(rb.gameObject, rb.gameObject.transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.gameObject.GetComponentInParent<Rigidbody>();
        if (rb == null)
            return;

        rb.velocity = Vector3.zero;
        if (rb.gameObject.CompareTag("movable"))
        {
            RaycastHit dropHit;
            if (Physics.Raycast(transform.forward + rb.gameObject.transform.position, -rb.gameObject.transform.up, out dropHit))
            {
                Vector3 pos = dropHit.collider.gameObject.transform.position;
                rb.gameObject.transform.position = new Vector3(pos.x, rb.gameObject.transform.position.y, pos.z);
                rb.gameObject.transform.rotation = objectRotationPair[rb.gameObject];
                objectRotationPair.Remove(rb.gameObject);
            }
        }
    }

}
