using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public AudioSource winSource;

    [Header("SFX Clips")]
    public AudioClip jumpSound;
    public AudioClip hitSound;

    [Header("BGM")]
    public AudioClip menuBGM;
    public AudioClip gameBGM;

    [Header("Music Toggles")]
    public bool isMenuMusicEnabled = true;
    public bool isGameMusicEnabled = true;

    [Header("Volume")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.3f;

    [Header("Advanced Audio")]
    public float bgmFadeSpeed = 2f;

    Coroutine fadeCoroutine;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

            LoadSettings();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetupAudioSources();

        UpdateVolume();

        HandleMusicByScene();
    }

    void SetupAudioSources()
    {
        // BGM Settings
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        // Cleaner SFX settings
        sfxSource.playOnAwake = false;
        winSource.playOnAwake = false;

        // Prevent harsh 3D sound behavior
        bgmSource.spatialBlend = 0f;
        sfxSource.spatialBlend = 0f;
        winSource.spatialBlend = 0f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusicByScene();
    }

    void HandleMusicByScene()
    {
        string scene = SceneManager.GetActiveScene().name;

        bool isMenuScene =
            scene == "MainMenu" ||
            scene == "LevelSelect";

        // Stop win sound in menus
        if (isMenuScene &&
            winSource != null &&
            winSource.isPlaying)
        {
            winSource.Stop();
        }

        bool shouldPlay =
            isMenuScene ?
            isMenuMusicEnabled :
            isGameMusicEnabled;

        if (shouldPlay)
            PlayBGM();
        else
            StopBGM();
    }

    // =========================
    // 🎵 MUSIC
    // =========================

    public void PlayBGM()
    {
        string scene =
            SceneManager.GetActiveScene().name;

        AudioClip targetClip =
            (scene == "MainMenu" ||
             scene == "LevelSelect")
            ? menuBGM
            : gameBGM;

        // Change clip only if needed
        if (bgmSource.clip != targetClip)
        {
            bgmSource.clip = targetClip;
            bgmSource.Play();
        }

        // Resume if stopped
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }

        // Smooth fade-in
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine =
            StartCoroutine(FadeBGM(
                bgmSource.volume,
                masterVolume * musicVolume
            ));
    }

    public void StopBGM()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine =
            StartCoroutine(FadeBGM(
                bgmSource.volume,
                0f,
                true
            ));
    }

    IEnumerator FadeBGM(
        float start,
        float target,
        bool stopAfterFade = false)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.unscaledDeltaTime * bgmFadeSpeed;

            bgmSource.volume =
                Mathf.Lerp(start, target, time);

            yield return null;
        }

        bgmSource.volume = target;

        if (stopAfterFade)
        {
            bgmSource.Stop();
        }
    }

    // =========================
    // 🔊 SFX
    // =========================

    public void PlayJump()
    {
        if (jumpSound == null) return;

        // Small random pitch variation
        sfxSource.pitch =
            Random.Range(0.96f, 1.04f);

        sfxSource.PlayOneShot(
            jumpSound,
            masterVolume * sfxVolume * 0.8f
        );

        Invoke(nameof(ResetPitch), 0.05f);
    }

    void ResetPitch()
    {
        sfxSource.pitch = 1f;
    }

    public void PlayHit()
    {
        if (hitSound == null) return;

        sfxSource.pitch =
            Random.Range(0.95f, 1.05f);

        sfxSource.PlayOneShot(
            hitSound,
            masterVolume * sfxVolume
        );

        Invoke(nameof(ResetPitch), 0.05f);
    }

    public void PlayWin()
    {
        string scene =
            SceneManager.GetActiveScene().name;

        // Prevent menu playback
        if (scene == "MainMenu" ||
            scene == "LevelSelect")
            return;

        if (winSource == null ||
            winSource.clip == null)
            return;

        // Stop previous play to prevent overlap
        winSource.Stop();

        winSource.pitch =
            Random.Range(0.98f, 1.02f);

        winSource.volume =
            masterVolume * sfxVolume;

        winSource.Play();
    }

    // =========================
    // 🎚️ VOLUME
    // =========================

    public void SetMasterVolume(float value)
    {
        masterVolume = value;

        UpdateVolume();

        SaveSettings();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;

        UpdateVolume();

        SaveSettings();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;

        UpdateVolume();

        SaveSettings();
    }

    void UpdateVolume()
    {
        sfxSource.volume =
            masterVolume * sfxVolume;

        bgmSource.volume =
            masterVolume * musicVolume;

        winSource.volume =
            masterVolume * sfxVolume;
    }

    // =========================
    // 💾 SAVE / LOAD
    // =========================

    void SaveSettings()
    {
        PlayerPrefs.SetInt(
            "MenuMusic",
            isMenuMusicEnabled ? 1 : 0);

        PlayerPrefs.SetInt(
            "GameMusic",
            isGameMusicEnabled ? 1 : 0);

        PlayerPrefs.SetFloat(
            "MasterVolume",
            masterVolume);

        PlayerPrefs.SetFloat(
            "SFXVolume",
            sfxVolume);

        PlayerPrefs.SetFloat(
            "MusicVolume",
            musicVolume);

        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        isMenuMusicEnabled =
            PlayerPrefs.GetInt(
                "MenuMusic",
                1) == 1;

        isGameMusicEnabled =
            PlayerPrefs.GetInt(
                "GameMusic",
                1) == 1;

        masterVolume =
            PlayerPrefs.GetFloat(
                "MasterVolume",
                1f);

        sfxVolume =
            PlayerPrefs.GetFloat(
                "SFXVolume",
                1f);

        musicVolume =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                0.3f);
    }

    // =========================
    // 🎛️ TOGGLES
    // =========================

    public void ToggleMenuMusic(bool state)
    {
        isMenuMusicEnabled = state;

        SaveSettings();

        HandleMusicByScene();
    }

    public void ToggleGameMusic(bool state)
    {
        isGameMusicEnabled = state;

        SaveSettings();

        HandleMusicByScene();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}