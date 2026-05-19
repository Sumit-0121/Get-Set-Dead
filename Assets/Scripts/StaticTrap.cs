using UnityEngine;

public class StaticTrap : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Invoke(nameof(UpdateState), 0.05f);
    }

    void UpdateState()
    {
        int attempt = GameManager.Instance.attemptCount;

        if (attempt == 1)
        {
            sr.enabled = true;
            col.enabled = true;
        }
        else
        {
            sr.enabled = false;
            col.enabled = false;
        }
    }
}