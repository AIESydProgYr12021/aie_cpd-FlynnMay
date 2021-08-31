using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    public float speed = 1.0f;

    //Rigidbody rb;
    LerpToVector lerpToVector;
    FollowPath followPath;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        followPath = GetComponent<FollowPath>();
        lerpToVector = followPath.lerpToVector;
        lerpToVector.lerpSpeed = speed;
        followPath.OnPathEnd.Add(DestroyBeam);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void DestroyBeam()
    {
        Destroy(gameObject);
        
    }
}
