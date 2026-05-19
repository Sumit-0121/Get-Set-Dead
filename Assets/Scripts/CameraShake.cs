using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    private float timer;

    void Awake()
    {
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        timer = duration;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0)
            {
                noise.AmplitudeGain = 0f;
            }
        }
    }
}