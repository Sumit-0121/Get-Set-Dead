using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveSpeed = 5f;

    Vector3 targetPosition;

    bool shouldMove = false;

    void Start()
    {
        targetPosition = transform.position +
                         Vector3.right * moveDistance;
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void ActivateTrap()
    {
        shouldMove = true;
    }
}