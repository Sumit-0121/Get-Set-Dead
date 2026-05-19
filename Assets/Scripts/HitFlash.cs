using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    public Image flashImage;
    private Coroutine currentFlash;

    public void Flash()
    {
        if (currentFlash != null)
            StopCoroutine(currentFlash);

        currentFlash = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        flashImage.color = new Color(1, 0, 0, 0.8f);

        yield return new WaitForSecondsRealtime(0.2f);

        float t = 0f;
        float duration = 0.3f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0.8f, 0f, t / duration);
            flashImage.color = new Color(1, 0, 0, alpha);
            yield return null;
        }

        flashImage.color = new Color(1, 0, 0, 0f);
        currentFlash = null;
    }
}