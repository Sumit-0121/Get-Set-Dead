using UnityEngine;

public class SpikeHunter : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Left-right movement
        float moveX = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        transform.position = new Vector3(
            startPosition.x + moveX,
            startPosition.y,
            startPosition.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kill player instantly
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDeath>().Die();
        }
    }
}