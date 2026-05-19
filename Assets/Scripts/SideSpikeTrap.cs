using System.Collections;
using UnityEngine;

public class SideSpikeTrap : MonoBehaviour
{
    [Header("Spike")]
    public Transform spike;

    [Header("Movement")]
    public float moveDistance = 2f;

    public float moveSpeed = 12f;

    public float stayTime = 1f;

    private Vector3 hiddenPosition;
    private Vector3 attackPosition;

    private bool activated = false;

    private void Start()
    {
        // Hidden starting position
        hiddenPosition = spike.position;

        // Attack position
        attackPosition =
            hiddenPosition + Vector3.left * moveDistance;
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
                spike.position,
                target
            ) > 0.01f
        )
        {
            spike.position = Vector3.MoveTowards(
                spike.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        spike.position = target;
    }
}