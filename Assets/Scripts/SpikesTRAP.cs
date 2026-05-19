using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeath playerDeath =
                collision.GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                playerDeath.Die();
            }
        }
    }
}