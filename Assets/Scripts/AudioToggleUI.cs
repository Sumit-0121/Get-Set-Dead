using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioToggleUI : MonoBehaviour
{
    public Image menuIcon;
    public Image gameIcon;

    public Sprite menuOn;
    public Sprite menuOff;

    public Sprite gameOn;
    public Sprite gameOff;

    void Start()
    {
        UpdateIcons();
         if (SceneManager.GetActiveScene().name != "MainMenu")
    {
        menuIcon.gameObject.SetActive(false);
    }
    }

    public void ToggleMenuMusic()
    {
        bool state = !AudioManager.instance.isMenuMusicEnabled;
        AudioManager.instance.ToggleMenuMusic(state);
        UpdateIcons();
    }

    public void ToggleGameMusic()
    {
        bool state = !AudioManager.instance.isGameMusicEnabled;
        AudioManager.instance.ToggleGameMusic(state);
        UpdateIcons();
    }

    void UpdateIcons()
    {
        menuIcon.sprite = AudioManager.instance.isMenuMusicEnabled ? menuOn : menuOff;
        gameIcon.sprite = AudioManager.instance.isGameMusicEnabled ? gameOn : gameOff;
    }
}