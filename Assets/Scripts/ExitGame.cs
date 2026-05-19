using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitPopup;

    public void OnExitPressed()
    {
        exitPopup.SetActive(true);
    }

    public void OnConfirmExit()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void OnCancelExit()
    {
        exitPopup.SetActive(false);
    }
}