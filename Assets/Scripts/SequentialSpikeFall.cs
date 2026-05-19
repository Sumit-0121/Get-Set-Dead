using System.Collections;
using UnityEngine;

public class SequentialSpikeFall : MonoBehaviour
{
    [Header("All Roof Spikes")]
    public Rigidbody2D[] spikes;

    [Header("Timing")]
    public float delayBetweenSpikes = 0.15f;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        if (collision.CompareTag("Player"))
        {
            activated = true;

            StartCoroutine(DropSpikes());
        }
    }

    IEnumerator DropSpikes()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].simulated = true;

            yield return new WaitForSeconds(delayBetweenSpikes);
        }
    }
}