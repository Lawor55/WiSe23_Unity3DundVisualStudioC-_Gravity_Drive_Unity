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
        //int[] highscores = new int[10];
        List<int> highscores = new();
        //Time.timeScale = 1;
        for (int i = 0; i < 10; i++)
        {
            //highscores[i] = PlayerPrefs.GetInt($"Highscore{i}", 0);
            highscores.Add(PlayerPrefs.GetInt($"Highscore{i}", 0));
            highscoreTexts[i].text = $"Score {i + 1}: {highscores[i].ToString("00000")}";
        }
        int scoreLastRun = PlayerPrefs.GetInt("LastScore", 0);
        lastScore.text = $"My Score: {scoreLastRun.ToString("00000")}";

        if (highscores.Contains(scoreLastRun))
        {
            highscoreTexts[highscores.IndexOf(scoreLastRun)].textStyle = TMP_Settings.defaultStyleSheet.GetStyle("c3");
        }
    }
}
