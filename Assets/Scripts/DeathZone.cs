using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Instantly kill player
        if (collision.CompareTag("Player"))
        {
            PlayerDeath playerDeath = collision.GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                playerDeath.restartDelay = 0f;

                playerDeath.Die();
            }
        }
    }
}