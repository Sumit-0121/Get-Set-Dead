using UnityEngine;

public class DeathEffectSpawner : MonoBehaviour
{
    public static DeathEffectSpawner instance;

    public GameObject deathEffectPrefab;

    void Awake()
    {
        instance = this;
    }

    public void SpawnEffect(Vector3 position)
{
    if (deathEffectPrefab == null)
    {
        Debug.LogError("DeathEffectPrefab NOT assigned!");
        return;
    }

    GameObject effect = Instantiate(deathEffectPrefab, position, Quaternion.identity);

    ParticleSystem ps = effect.GetComponent<ParticleSystem>();
    if (ps != null)
    {
        ps.Clear();   // reset
        ps.Play();    // force play
    }
}
}