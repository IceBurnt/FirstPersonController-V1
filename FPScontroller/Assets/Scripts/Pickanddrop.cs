using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickanddrop : MonoBehaviour

    
{
   
    
    public float pickUpDistance = 2f;
    
    [SerializeField] public LayerMask WhatIsTrash;
    [SerializeField] public Transform camTransform;
    [SerializeField] public Transform objectGrabPointTransform;

    private Objectgrabbable objectgrabbable;


    void Update()
    {
        PickUpAndDrop();
    }

    private void PickUpAndDrop()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (objectgrabbable == null) // to grab
            {    
                if (Physics.SphereCast(camTransform.position, 0.3f, camTransform.forward, out RaycastHit raycasthit, pickUpDistance, WhatIsTrash))
                {
                    if (raycasthit.transform.TryGetComponent(out objectgrabbable))
                    {
                        objectgrabbable.Grab(objectGrabPointTransform);
                        Debug.Log("hit");
                    }
                }
            }   
            else //to release
            {
                objectgrabbable.Drop();
                objectgrabbable = null;
            }
        }
    }
}
