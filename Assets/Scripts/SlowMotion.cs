using UnityEngine;
using System.Collections;

public class SlowMotion : MonoBehaviour
{
    public void DoSlowMotion(float scale, float duration)
    {
        StartCoroutine(SlowRoutine(scale, duration));
    }

    IEnumerator SlowRoutine(float scale, float duration)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * scale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}