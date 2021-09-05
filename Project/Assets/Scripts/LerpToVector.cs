using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LerpToVector : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 currentPosition;
    public List<Action> OnTargetReached = new List<Action>();

    public float lerpTime = 0.0f;
    public float lerpSpeed = 5.0f;

    // Start is called before the first frame update
    public void Awake()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {

        lerpTime += Time.deltaTime * lerpSpeed;

        if (lerpTime > 1.0f)
        {
            lerpTime = 1.0f;
            currentPosition = targetPosition;
            targetPosition = currentPosition;

            foreach (var act in OnTargetReached)
            {
                act?.Invoke();
            }
        }

        transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpTime);
    }

    public bool GetCanMove()
    {
        return targetPosition == currentPosition;
    }
}
