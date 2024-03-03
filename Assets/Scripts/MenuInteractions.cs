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
    private AudioSource audioSource;
    private float musicVolume;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        print("Restart!");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Credits()
    {
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

    public void Quit()
    {
        Application.Quit();
        print("Quiting Game");
    }
}
