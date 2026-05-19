using System.Collections;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    [Header("Spike References")]
    public Transform spike1;
    public Transform spike2;
    public Transform spike3;

    [Header("Movement")]
    public float moveDistance = 8f;
    public float moveSpeed = 8f;

    [Header("Delay")]
    public float delayBetweenSpikes = 0.5f;

    // Shared across deaths/reloads
    private static int globalAttempt = 0;

    private bool activated;

    private Vector3 spike1Start;
    private Vector3 spike2Start;
    private Vector3 spike3Start;

    private void Start()
    {
        spike1Start = spike1.position;
        spike2Start = spike2.position;
        spike3Start = spike3.position;
    }

    public void ActivateTrap()
    {
        if (activated) return;

        activated = true;

        globalAttempt++;

        Debug.Log("Attempt Number: " + globalAttempt);

        if (globalAttempt == 1)
        {
            StartCoroutine(AllAttack());
        }
        else if (globalAttempt == 2)
        {
            StartCoroutine(SequentialAttack());
        }
        else
        {
            StartCoroutine(RandomAttack());
        }
    }

    // ATTEMPT 1
    IEnumerator AllAttack()
    {
        StartCoroutine(MoveSpike(spike1, spike1Start));
        StartCoroutine(MoveSpike(spike2, spike2Start));
        StartCoroutine(MoveSpike(spike3, spike3Start));

        yield return null;
    }

    // ATTEMPT 2
    IEnumerator SequentialAttack()
    {
        yield return StartCoroutine(MoveSpike(spike1, spike1Start));

        yield return new WaitForSeconds(delayBetweenSpikes);

        yield return StartCoroutine(MoveSpike(spike2, spike2Start));

        yield return new WaitForSeconds(delayBetweenSpikes);

        yield return StartCoroutine(MoveSpike(spike3, spike3Start));
    }

    // ATTEMPT 3+
    IEnumerator RandomAttack()
    {
        Transform[] spikes = { spike1, spike2, spike3 };
        Vector3[] starts = { spike1Start, spike2Start, spike3Start };

        bool[] used = new bool[3];

        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, 3);
            }
            while (used[randomIndex]);

            used[randomIndex] = true;

            yield return StartCoroutine(
                MoveSpike(spikes[randomIndex], starts[randomIndex])
            );

            yield return new WaitForSeconds(
                Random.Range(0.2f, 1f)
            );
        }
    }

    IEnumerator MoveSpike(Transform spike, Vector3 startPos)
    {
        Vector3 target = startPos + Vector3.left * moveDistance;

        while (Vector2.Distance(spike.position, target) > 0.05f)
        {
            spike.position = Vector2.MoveTowards(
                spike.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}