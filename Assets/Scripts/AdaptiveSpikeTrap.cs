using System.Collections;
using UnityEngine;

public class AdaptiveSpikeTrap : MonoBehaviour
{
    [Header("Spike Objects")]
    public Transform[] spikes;

    [Header("Pop Settings")]
    public float popUpHeight = 1f;

    public float spikeMoveSpeed = 4f;

    public float delayBetweenSpikes = 1f;

    [Header("Group Movement")]
    public float horizontalMoveDistance = 1f;

    public float horizontalMoveSpeed = 2f;

    public float waitTimeAtSides = 1f;

    [Header("Random Attempt")]
    public float randomPopDelay = 0.2f;

    public float spikeStayDuration = 0.3f;

    private Vector3[] hiddenPositions;
    private Vector3[] shownPositions;

    private bool activated = false;

    private void Start()
{
    hiddenPositions = new Vector3[spikes.Length];
    shownPositions = new Vector3[spikes.Length];

    for (int i = 0; i < spikes.Length; i++)
    {
        hiddenPositions[i] = spikes[i].position;

        shownPositions[i] =
            hiddenPositions[i] + Vector3.up * popUpHeight;
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        if (collision.CompareTag("Player"))
        {
            activated = true;

            int attempt = GameManager.Instance.attemptCount;

            // Attempt 1
            if (attempt == 1)
            {
                StartCoroutine(AttemptOne());
            }

            // Attempt 2
            else if (attempt == 2)
            {
                StartCoroutine(AttemptTwo());
            }

            // Attempt 3+
            else
            {
                StartCoroutine(AttemptThree());
            }
        }
    }

    // =========================================================
    // ATTEMPT 1
    // Sequential spikes
    // =========================================================

    IEnumerator AttemptOne()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            yield return StartCoroutine(
                MoveSpike(
                    spikes[i],
                    shownPositions[i],
                    spikeMoveSpeed
                )
            );

            yield return new WaitForSeconds(delayBetweenSpikes);
        }
    }

    // =========================================================
    // ATTEMPT 2
    // All spikes emerge + move left/right with pauses
    // =========================================================

    IEnumerator AttemptTwo()
    {
        // Make all spikes come out together
        for (int i = 0; i < spikes.Length; i++)
        {
            StartCoroutine(
                MoveSpike(
                    spikes[i],
                    shownPositions[i],
                    spikeMoveSpeed
                )
            );
        }

        yield return new WaitForSeconds(1.5f);

        Vector3 startPos = transform.position;

        Vector3 leftPos =
            startPos + Vector3.left * horizontalMoveDistance;

        Vector3 rightPos =
            startPos + Vector3.right * horizontalMoveDistance;

        while (true)
        {
            // Move LEFT
            yield return StartCoroutine(
                MoveGroup(leftPos)
            );

            // Wait
            yield return new WaitForSeconds(waitTimeAtSides);

            // Move RIGHT
            yield return StartCoroutine(
                MoveGroup(rightPos)
            );

            // Wait
            yield return new WaitForSeconds(waitTimeAtSides);
        }
    }

    // =========================================================
    // ATTEMPT 3
    // Random continuous spikes
    // =========================================================

    IEnumerator AttemptThree()
{
    while (true)
    {
        // Random number of spikes
        int randomSpikeCount =
            Random.Range(1, spikes.Length + 1);

        for (int i = 0; i < randomSpikeCount; i++)
        {
            int randomIndex =
                Random.Range(0, spikes.Length);

            StartCoroutine(
                RandomSpikePop(randomIndex)
            );
        }

        // Random delay
        float randomDelay =
            Random.Range(0.1f, 0.6f);

        yield return new WaitForSeconds(randomDelay);
    }
}

    // =========================================================
    // RANDOM SPIKE POP
    // =========================================================

   IEnumerator RandomSpikePop(int index)
{
    // Move UP
    yield return StartCoroutine(
        MoveSpike(
            spikes[index],
            shownPositions[index],
            spikeMoveSpeed
        )
    );

    // Random visible duration
    float stayTime =
        Random.Range(0.1f, 0.5f);

    yield return new WaitForSeconds(stayTime);

    // Move DOWN
    yield return StartCoroutine(
        MoveSpike(
            spikes[index],
            hiddenPositions[index],
            spikeMoveSpeed
        )
    );
}

    // =========================================================
    // MOVE SINGLE SPIKE
    // =========================================================

    IEnumerator MoveSpike(
        Transform spike,
        Vector3 target,
        float speed
    )
    {
        while (
            Vector3.Distance(
                spike.position,
                target
            ) > 0.01f
        )
        {
            spike.position = Vector3.MoveTowards(
                spike.position,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        spike.position = target;
    }

    // =========================================================
    // MOVE WHOLE GROUP
    // =========================================================

    IEnumerator MoveGroup(Vector3 target)
    {
        while (
            Vector3.Distance(
                transform.position,
                target
            ) > 0.01f
        )
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                horizontalMoveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = target;
    }
}