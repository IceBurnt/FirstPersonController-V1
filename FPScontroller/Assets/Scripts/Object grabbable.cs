using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectgrabbable : MonoBehaviour
{

    private Rigidbody rb;
    Transform objectGrabPointTransform;

    

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpspeed = 20f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpspeed);
            rb.MovePosition(newPosition);
            Debug.Log("Picked");

            
        }
    }
}
