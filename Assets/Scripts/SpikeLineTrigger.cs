using UnityEngine;

public class SpikeLineTrigger : MonoBehaviour
{
    public AdaptiveSpikeLine spikeLine;

    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            spikeLine.ActivateSpike();
        }
    }
}