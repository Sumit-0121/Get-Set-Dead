using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public float moveSpeed = 40f;

    int attempt;

    bool moveRight = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        attempt = GameManager.Instance.attemptCount;

        // ATTEMPT 1
        // Fake platform
        if (attempt == 1)
        {
            boxCollider.enabled = false;
        }

        // ATTEMPT 2+
        // Normal platform
        else
        {
            boxCollider.enabled = true;
        }
    }

    void Update()
    {
        // ATTEMPT 3+
        if (moveRight)
        {
            transform.Translate(
                Vector2.right * moveSpeed * Time.deltaTime
            );
        }
    }

    public void ActivateTrap()
    {
        // ONLY ATTEMPT 3+
        if (attempt >= 3)
        {
            moveRight = true;
        }
    }
}