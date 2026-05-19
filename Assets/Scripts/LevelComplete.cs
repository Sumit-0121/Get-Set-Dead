using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (completed) return;

        if (collision.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        completed = true;

        Debug.Log("LEVEL COMPLETE");

        // Current level number
        int currentLevel =
            SceneManager.GetActiveScene().buildIndex - 1;

        // Unlock next level
        int nextUnlockedLevel = currentLevel + 1;

        // Save ONLY if greater
        if (nextUnlockedLevel >
            PlayerPrefs.GetInt("UnlockedLevel", 1))
        {
            PlayerPrefs.SetInt(
                "UnlockedLevel",
                nextUnlockedLevel
            );

            PlayerPrefs.Save();

            Debug.Log(
                "Unlocked Level = " +
                nextUnlockedLevel
            );
        }

        // WIN SOUND
        if (AudioManager.instance != null)
            AudioManager.instance.PlayWin();

        // Small finish scale effect
        transform.localScale *= 1.2f;

        // Freeze + transition
        StartCoroutine(LevelCompleteRoutine());
    }

    IEnumerator LevelCompleteRoutine()
    {
        // Freeze frame effect
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1f;

        // Reset attempts for next level
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetAttempts();
        }

        // Next level index
        int next =
            SceneManager.GetActiveScene().buildIndex + 1;

        // Load next level with transition
        if (ScreenTransition.instance != null)
        {
            ScreenTransition.instance.FadeAndLoad(next, 1f);
        }
        else
        {
            SceneManager.LoadScene(next);
        }
    }
}