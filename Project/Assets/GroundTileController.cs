using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileController : MonoBehaviour
{
    public GameObject moveSpotIndicator;
    public ParticleSystem indicatorParticle;
    GameObject instanceIndicator;
    bool wasNull = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        indicatorParticle = moveSpotIndicator.GetComponent<ParticleSystem>();

        if (indicatorParticle == null)
        {
            wasNull = true;
            return;
        }

        if (indicatorParticle.IsAlive())
        {
            instanceIndicator = Instantiate(moveSpotIndicator);
            instanceIndicator.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }
        else
        {
            Destroy(moveSpotIndicator);
        }
    }
}
