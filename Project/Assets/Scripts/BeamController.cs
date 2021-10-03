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
        GlobalControl.Instance.audioManager.Play("Wind");
        followPath = GetComponent<FollowPath>();
        lerpToVector = followPath.lerpToVector;
        lerpToVector.lerpSpeed = speed;
        followPath.OnPathEnd.Add(DestroyBeam);
        lerpToVector.OnTargetReached.Add(PlayWind);
        PlayWind();
    }

    private void PlayWind()
    {
        GlobalControl.Instance.audioManager.Play("Wind");
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        if(followPath.path.Count == followPath.pathIterator)
        {
            var trails = GetComponentsInChildren<TrailRenderer>();
            foreach (var trail in trails)
            {
                var color = trail.material.color;
                trail.material.color = new Color(color.r, color.g, color.b, 1.0f - lerpToVector.lerpTime);
            }
        }
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
