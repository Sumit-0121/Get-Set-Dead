using UnityEngine;

public class AdaptiveSpikeLine : MonoBehaviour
{
    public float moveDistance = 7f;
    public float moveSpeed = 5f;

    bool moving = false;

    Vector3 targetPos;

    int currentAttempt;

    void Start()
    {
        currentAttempt = PlayerPrefs.GetInt("AttemptCount", 1);
        targetPos = transform.position + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void ActivateSpike()
    {
        // Only move on attempt 2+
        if (currentAttempt >= 2)
        {
            moving = true;
        }
    }
}