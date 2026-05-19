using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject exitPanel;

    public void StartGame()
    {
        ScreenTransition.instance.FadeAndLoad(1, 1f);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        exitPanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }
}