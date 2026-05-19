using UnityEngine;

public class VanishTrap : MonoBehaviour
{
    public float triggerDistance = 3f;
    public float ambushOffset = 1.5f;

    private SpriteRenderer sr;
    private Collider2D col;
    private Transform player;

    private bool activeTrap = false;
    private bool ambushed = false;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Invoke(nameof(UpdateState), 0.05f);
    }

    void UpdateState()
    {
        int attempt = GameManager.Instance.attemptCount;

        if (attempt >= 3)
        {
            activeTrap = true;
            player = GameObject.FindGameObjectWithTag("Player").transform;

            sr.enabled = false;
            col.enabled = false;
        }
        else
        {
            activeTrap = false;
            sr.enabled = false;
            col.enabled = false;
        }
    }

    void Update()
    {
        if (!activeTrap || ambushed || player == null) return;

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        if (distance < triggerDistance)
        {
            Ambush();
        }
    }

    void Ambush()
    {
        ambushed = true;

        float spawnX = player.position.x + ambushOffset;

        transform.position = new Vector3(
            spawnX,
            transform.position.y,
            transform.position.z
        );

        sr.enabled = true;
        col.enabled = true;
    }
}