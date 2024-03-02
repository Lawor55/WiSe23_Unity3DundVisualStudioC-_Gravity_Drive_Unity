using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreList : MonoBehaviour
{
    [SerializeField] private TMP_Text[] highscoreTexts;
    [SerializeField] private TMP_Text lastScore;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        for (int i = 0; i < 10; i++)
        {
            highscoreTexts[i].text = $"Score: {PlayerPrefs.GetInt($"Highscore{i}", 0).ToString("00000")}";
        }
        lastScore.text = $"Score: {PlayerPrefs.GetInt("LastScore", 0).ToString("00000")}";
    }
}
