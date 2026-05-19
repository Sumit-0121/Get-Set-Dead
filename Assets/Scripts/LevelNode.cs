using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject lockIcon;
    public Button button;

    private int levelNumber;

   public void Setup(int index)
{
    levelNumber = index;

    levelText.text = index.ToString();

    int unlockedLevel =
        PlayerPrefs.GetInt("UnlockedLevel", 1);

    Debug.Log(
        "Node = " + levelNumber +
        " | Saved Unlock = " +
        unlockedLevel
    );

    button.onClick.RemoveAllListeners();

    bool isUnlocked =
        levelNumber <= unlockedLevel;

    button.interactable = isUnlocked;

    if (lockIcon != null)
    {
        lockIcon.SetActive(!isUnlocked);
    }

    if (isUnlocked)
    {
        button.onClick.AddListener(OpenLevel);
    }
}

    void OpenLevel()
    {
        // Level1 = build index 2
        SceneManager.LoadScene(levelNumber + 1);
    }
}