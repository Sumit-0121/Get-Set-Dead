using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PLAYER PREFS RESET");
    }
}