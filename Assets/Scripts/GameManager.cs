using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int attemptCount = 1;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterDeath()
    {
        attemptCount++;
        Debug.Log("Attempt: " + attemptCount);
    }

    public void ResetAttempts()
    {
        attemptCount = 1;
    }
}