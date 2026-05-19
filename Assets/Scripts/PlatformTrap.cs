using UnityEngine;
using System.Collections;

public class PlatformTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private bool triggered = false;
    private bool moveUp = false;

    public float upwardSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if (moveUp)
        {
            transform.Translate(Vector2.up * upwardSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (triggered) return;

    if (collision.gameObject.CompareTag("Player"))
    {
        triggered = true;

        int attempt = GameManager.Instance.attemptCount;

        if (attempt == 1)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (attempt == 2)
        {
            moveUp = true;
        }
        else
        {
            StartCoroutine(VanishPlatform());
        }
    }
}

    IEnumerator VanishPlatform()
    {
        sr.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(0.5f);

        sr.enabled = true;
        col.enabled = true;
    }
}