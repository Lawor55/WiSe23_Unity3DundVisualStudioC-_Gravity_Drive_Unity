using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //plays more calm part of song in the Highscore List
        if (audioSource.time > 193 || audioSource.time < 186 && SceneManager.GetActiveScene().name == "HighScoreList")
        {
            audioSource.time = 186;
        }
        //loops the active part of the song for the gameplay
        if (audioSource.time > 184 && SceneManager.GetActiveScene().name == "MainScene")
        {
            audioSource.time = 44;
        }
        //print(Time.realtimeSinceStartup);
    }
}
