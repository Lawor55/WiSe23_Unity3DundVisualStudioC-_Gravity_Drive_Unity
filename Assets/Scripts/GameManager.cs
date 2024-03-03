using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //the default 100% of fuel for the player
    public static float fuel = 100f;
    [Header("Fuel Settings:")]
    //the speed at which the player looses fuel
    [SerializeField] [Range(-10f, 100f)] float fuelConsumptionPerSecond;
    private Slider fuelSlider;

    public static bool gameOver = false;
    [SerializeField] private AnimationCurve speedCurve;
    private float timeSinceSceneRefresh;
    public static float gameSpeed;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        fuelSlider = GameObject.Find("UI_Canvas").GetComponentInChildren<Slider>();
        //create a Parent object for all levelModules so its easier to understand hirachy
        scoreManager = GetComponent<ScoreManager>();
        //print($"Value 1: {speedCurve[0].value} Value 2: {speedCurve[1].value}");
    }

    // Update is called once per frame
    void Update()
    {
        //slowly reduce the fuel over time
        fuel -= fuelConsumptionPerSecond * Time.deltaTime;
        fuel = Mathf.Clamp(fuel, 0, 100);
        fuelSlider.value = fuel;
        if (fuel <= 0 && !gameOver)
        {
            GameOver();
        }
        GameSpeed();
    }

    public void GameOver()
    {
        print("Game Over!");
        //Time.timeScale = 0;
        gameOver = true;
        scoreManager.SaveHighscore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void GameSpeed()
    {
        timeSinceSceneRefresh += Time.deltaTime;
        timeSinceSceneRefresh = Mathf.Clamp(timeSinceSceneRefresh, 0, speedCurve[1].time);
        //gameSpeed = 1 + (ScoreManager.score / (1000 * scoreManager.scorePerSecond));
        gameSpeed = speedCurve.Evaluate(timeSinceSceneRefresh);
        //print($"Gamespeed: {gameSpeed}\nTime since Start: {Time.realtimeSinceStartup}");
    }
}
