using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteractions : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioClip uiInteractionSound;
    private AudioSource audioSource;
    private float musicVolume;

    private void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        audioSource = FindObjectOfType<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        audioSource.volume = musicVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = musicVolume;
        }
    }

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    print("Scene Loaded");
    //    audioSource = FindObjectOfType<AudioSource>();
    //    musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
    //    audioSource.volume = musicVolume;
    //    if (volumeSlider != null)
    //    {
    //        volumeSlider.value = musicVolume;
    //    }
    //}

    public void Restart()
    {
        audioSource.PlayOneShot(uiInteractionSound);
        SceneManager.LoadScene("MainScene");
        print("Restart!");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Credits()
    {
        audioSource.PlayOneShot(uiInteractionSound);
        creditsCanvas.SetActive(!creditsCanvas.activeSelf);
        volumeSlider.gameObject.SetActive(!creditsCanvas.activeSelf);
        startButton.SetActive(!creditsCanvas.activeSelf);
    }

    public void OnValueChangedMusic(float newValue)
    {
        musicVolume = newValue;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        audioSource.volume = musicVolume;
    }

    public void TitleScreen()
    {
        audioSource.PlayOneShot(uiInteractionSound);
        Destroy(audioSource.gameObject);
        SceneManager.LoadScene("Titlescreen");
        print("Restart!");
    }

    public void Quit()
    {
        audioSource.PlayOneShot(uiInteractionSound);
        Application.Quit();
        print("Quiting Game");
    }
}
