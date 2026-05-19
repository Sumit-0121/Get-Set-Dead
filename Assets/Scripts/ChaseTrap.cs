using UnityEngine;

public class ChaseTrap : MonoBehaviour
{
    public float moveSpeed = 27f;

    private SpriteRenderer sr;
    private Collider2D col;

    private bool activeTrap = false;
    private float moveDirection = 4f; // -1 left, +1 right

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Invoke(nameof(UpdateState), 0.05f);
    }

    void UpdateState()
    {
        int attempt = GameManager.Instance.attemptCount;

        if (attempt == 2)
        {
            ActivateTrap();
        }
        else
        {
            DeactivateTrap();
        }
    }

    void ActivateTrap()
    {
        activeTrap = true;
        sr.enabled = true;
        col.enabled = true;

        Transform player =
            GameObject.FindGameObjectWithTag("Player").transform;

        // Decide direction ONLY ONCE
        if (player.position.x > transform.position.x)
            moveDirection = 1f;
        else
            moveDirection = -1f;
    }

    void DeactivateTrap()
    {
        activeTrap = false;
        sr.enabled = false;
        col.enabled = false;
    }

    void Update()
    {
        if (!activeTrap) return;

        // Move straight horizontally
        transform.Translate(
            Vector2.right * moveDirection * moveSpeed * Time.deltaTime
        );
    }
}