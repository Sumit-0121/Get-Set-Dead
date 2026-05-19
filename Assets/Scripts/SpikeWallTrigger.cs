using UnityEngine;

public class SpikeWallTrigger : MonoBehaviour
{
    public SpikeWall spikeWall;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            spikeWall.ActivateTrap();
        }
    }
}