using System.Collections;
using UnityEngine;

public class SpikeLastTrigger : MonoBehaviour
{
    [Header("Spike Object")]
    public Transform spikeLast;

    [Header("Movement Settings")]
    public float moveDistance = 2f;

    public float moveSpeed = 12f;

    public float stayTime = 1f;

    private Vector3 hiddenPosition;
    private Vector3 attackPosition;

    private bool activated = false;

    private void Start()
    {
        // Hidden starting position
        hiddenPosition = spikeLast.position;

        // Pop-out position
        attackPosition =
            hiddenPosition + Vector3.up * moveDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        if (collision.CompareTag("Player"))
        {
            activated = true;

            StartCoroutine(SpikeRoutine());
        }
    }

    IEnumerator SpikeRoutine()
    {
        // Move OUT
        yield return StartCoroutine(
            MoveSpike(attackPosition)
        );

        // Stay outside
        yield return new WaitForSeconds(stayTime);

        // Move BACK
        yield return StartCoroutine(
            MoveSpike(hiddenPosition)
        );
    }

    IEnumerator MoveSpike(Vector3 target)
    {
        while (
            Vector3.Distance(
                spikeLast.position,
                target
            ) > 0.01f
        )
        {
            spikeLast.position = Vector3.MoveTowards(
                spikeLast.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        spikeLast.position = target;
    }
}