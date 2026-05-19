using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("Restart")]
    public float restartDelay = 1f;

    private bool isDead = false;

    [Header("Feedback")]
    public HitFlash hitFlash;
    public SlowMotion slowMotion;
    public CameraShake cameraShake;

    // Cached Components
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerMovement movement;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prevent multiple deaths
        if (isDead)
            return;

        if (collision.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
    {
        // Extra safety
        if (isDead)
            return;

        isDead = true;

        // Disable collider immediately
        if (playerCollider != null)
            playerCollider.enabled = false;

        // Spawn death effect
        if (DeathEffectSpawner.instance != null)
        {
            DeathEffectSpawner.instance.SpawnEffect(transform.position);
        }

        // Visual feedback
        if (hitFlash != null)
            hitFlash.Flash();

        // Slow motion
        if (slowMotion != null)
            slowMotion.DoSlowMotion(0.15f, 0.15f);

        // Camera shake
        if (cameraShake != null)
            cameraShake.Shake(6f, 0.3f);

        // Audio
        if (AudioManager.instance != null)
            AudioManager.instance.PlayHit();

        // Attempt Counter System
        int attempts = PlayerPrefs.GetInt("AttemptCount", 1);

        attempts++;

        PlayerPrefs.SetInt("AttemptCount", attempts);

        PlayerPrefs.Save();

        Debug.Log("Current Attempt: " + attempts);

        // Optional UI update
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterDeath();

        // Disable visuals
        if (sr != null)
            sr.enabled = false;

        // Stop movement
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Disable movement script
        if (movement != null)
            movement.enabled = false;

        // Restart level
        Invoke(nameof(RestartLevel), restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}