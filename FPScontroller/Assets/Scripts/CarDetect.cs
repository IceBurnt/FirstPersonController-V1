using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetection : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask carLayer;

    private GameObject detectedCar;
    private Trash trashScript;

    public bool isCrashed = false;

    private void Start()
    {
        trashScript = FindObjectOfType<Trash>();
    }

    private void Update()
    {
        DetectCar();
    }

    private void DetectCar()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, carLayer);
        if (hitColliders.Length > 0)
        {
            detectedCar = hitColliders[0].gameObject;
        }
        else
        {
            detectedCar = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            
           Trash.Score -= 3;
           isCrashed = true;
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {

            
            isCrashed = false;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
