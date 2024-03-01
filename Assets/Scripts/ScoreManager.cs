using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static float score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] [Range(1, 10)] private int scorePerSecond;

    // Update is called once per frame
    void Update()
    {
        score += scorePerSecond * Time.deltaTime;
        scoreText.text = $"Score: {score.ToString("00000")}";
    }
}
