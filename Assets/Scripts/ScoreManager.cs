using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static float score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] [Range(1, 10)] public int scorePerSecond;
    [SerializeField] private List<int> highscores = new();
    [SerializeField] private float testHighscoreToAdd;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            highscores.Add(PlayerPrefs.GetInt($"Highscore{i}", 0));
        }

        highscores.Sort();
        highscores.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        score += scorePerSecond * Time.deltaTime;
        scoreText.text = $"Score: {score.ToString("00000")}";
    }

    public void SaveHighscore()
    {
        highscores.Add(((int)score));
        highscores.Sort();
        highscores.Remove(0);
        highscores.Reverse();

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt($"Highscore{i}", highscores[i]);
        }
    }
}
