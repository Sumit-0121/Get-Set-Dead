using UnityEngine;

public class RoofSpikeTrigger : MonoBehaviour
{
    public AdaptiveRoofSpike roofSpike;

    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered)
            return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            roofSpike.ActivateTrap();
        }
    }
}