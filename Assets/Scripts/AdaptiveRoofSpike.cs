using UnityEngine;

public class AdaptiveRoofSpike : MonoBehaviour
{
    public float moveSpeed = 5f;

    bool moveDown = false;
    bool moveLeft = false;

    Vector3 targetDownPosition;
    Vector3 targetLeftPosition;

    int attempt;

    void Start()
    {
        attempt = GameManager.Instance.attemptCount;

        // ATTEMPT 2
        // Move downward by fixed amount
        targetDownPosition =
            transform.position + Vector3.down * 3f;

        // ATTEMPT 3
        targetLeftPosition =
            transform.position + Vector3.left * 2f;
    }

    void Update()
    {
        // ATTEMPT 2
        if (moveDown)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetDownPosition,
                moveSpeed * Time.deltaTime
            );
        }

        // ATTEMPT 3
        if (moveLeft)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetLeftPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void ActivateTrap()
    {
        // ATTEMPT 2
        if (attempt == 2)
        {
            moveDown = true;
        }

        // ATTEMPT 3+
        else if (attempt >= 3)
        {
            moveLeft = true;
        }
    }
}