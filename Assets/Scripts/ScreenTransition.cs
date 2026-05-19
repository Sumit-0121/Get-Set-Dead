using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition instance;

    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 🔥 VERY IMPORTANT
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeAndLoad(int sceneIndex, float waitTime)
    {
        StartCoroutine(FadeRoutine(sceneIndex, waitTime));
    }

    IEnumerator FadeRoutine(int sceneIndex, float waitTime)
    {
        float t = 0;

        // 🔴 FADE IN (to black)
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ⏳ WAIT
        yield return new WaitForSecondsRealtime(waitTime);

        // 🔄 LOAD NEXT SCENE
        SceneManager.LoadScene(sceneIndex);
        
        // 🔥 Wait a bit so scene fully renders
        yield return new WaitForSecondsRealtime(0.1f);
        
        // Reset timer
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}