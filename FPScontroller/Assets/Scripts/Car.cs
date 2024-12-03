using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float distanceToTravel = 100f;
    public float respawnDelay = 3f;
   


    private Vector3 startPosition;
    private Vector3 previousPosition;
    public Vector3 currentVelocity;

    private bool isMoving = false;

    private void Start()
    {
        startPosition = transform.position;
        previousPosition = startPosition;
        MoveCar();
    }

    private void Update()
    {
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void MoveCar()
    {
        if (!isMoving)
        {
            isMoving = true;
            Vector3 targetPosition = startPosition + transform.forward * distanceToTravel;
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    private System.Collections.IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(respawnDelay);
        RespawnCar();
    }

    private void RespawnCar()
    {
        transform.position = startPosition;
        isMoving = false;
        MoveCar();
    }

   
}
