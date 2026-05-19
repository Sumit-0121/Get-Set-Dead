using System.Collections;
using UnityEngine;

public class SpikeWaveTrap : MonoBehaviour
{
    [Header("Spike Objects")]
    public Transform[] spikes;

    [Header("Settings")]
    public float popUpHeight = 1.5f;

    public float spikeMoveSpeed = 8f;

    public float delayBetweenSpikes = 0.08f;

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

            StartCoroutine(ActivateSpikeWave());
        }
    }

    IEnumerator ActivateSpikeWave()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            StartCoroutine(
                MoveSpike(
                    spikes[i],
                    shownPositions[i]
                )
            );

            yield return new WaitForSeconds(delayBetweenSpikes);
        }
    }

    IEnumerator MoveSpike(Transform spike, Vector3 target)
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
                spikeMoveSpeed * Time.deltaTime
            );

            yield return null;
        }

        spike.position = target;
    }
}