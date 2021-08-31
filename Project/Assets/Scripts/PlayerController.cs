using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LerpToVector lerpToVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 moveDir = Vector3.zero;

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        ////assuming we only using the single camera:
        //var camera = Camera.main;

        ////camera forward and right vectors:
        //var forward = camera.transform.forward;
        //var right = camera.transform.right;

        ////project forward and right vectors on the horizontal plane (y = 0)
        //forward.y = 0f;
        //right.y = 0f;
        //forward.Normalize();
        //right.Normalize();

        ////this is the direction in the world space we want to move:
        //var desiredMoveDirection = (forward * verticalAxis + right * horizontalAxis);

        if (horizontalAxis > 0) // Forward
        {
            moveDir = transform.forward;
        }
        else if (horizontalAxis < 0) // Backwards
        {
            moveDir = -transform.forward;
        }
        else if (verticalAxis < 0) // Right
        {
            moveDir = transform.right;
        }
        else if (verticalAxis > 0) // Left
        {
            moveDir = -transform.right;
        }


        if (lerpToVector.GetCanMove())
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (child.CompareTag("movable"))
                    child.SetParent(null);
            }

            if (Physics.Raycast(transform.position, moveDir, out hit, 1) && moveDir != Vector3.zero)
            {
                GameObject parent = hit.collider.gameObject;

                while (parent.transform.parent != null || parent.transform.parent.CompareTag("movable"))
                    parent = parent.transform.parent.gameObject;

                SetChildOnCondition(parent, parent.CompareTag("movable"));
            }

            lerpToVector.targetPosition = transform.position + moveDir;
            lerpToVector.lerpTime = 0.0f;
        }
    }

    void SetChildOnCondition(GameObject obj, bool condition)
    {
        if (!condition)
            return;

        obj.transform.SetParent(transform);
    }
}
