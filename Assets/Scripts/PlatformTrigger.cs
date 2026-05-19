using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public FakePlatform fakePlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fakePlatform.ActivateTrap();
        }
    }
}