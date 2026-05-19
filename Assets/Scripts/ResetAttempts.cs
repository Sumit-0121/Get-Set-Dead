using UnityEngine;

public class ResetAttempts : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("AttemptCount", 1);

        PlayerPrefs.Save();

        Debug.Log("Attempts Reset");
    }
}