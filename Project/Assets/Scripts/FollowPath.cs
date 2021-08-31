using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowPath : MonoBehaviour
{
    public LerpToVector lerpToVector;
    public List<Vector3> path = new List<Vector3>();
    public List<Action> OnPathEnd = new List<Action>();
    int i = 0;

    void Awake()
    {
        lerpToVector = GetComponent<LerpToVector>();
        lerpToVector.currentPosition = transform.position;
        lerpToVector.targetPosition = transform.position;

        lerpToVector.OnTargetReached.Add(() => {
            if (i >= path.Count)
                return;

            lerpToVector.targetPosition = path[i];
            lerpToVector.lerpTime = 0;
            i++;

        }); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count <= 0)
        {
            PathComplete();
            return;
        }

        if (lerpToVector.currentPosition == path[path.Count - 1])
        {
            PathComplete();
        }
    }

    void PathComplete()
    {
        foreach (var act in OnPathEnd)
        {
            act?.Invoke();
        }
    }
}
