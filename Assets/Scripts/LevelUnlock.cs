using UnityEngine;

public class LevelUnlock : MonoBehaviour

{
    void Awake()
    {
        // First launch only
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            PlayerPrefs.Save();

            Debug.Log("Initialized Level Unlock Save");
        }
    }
}