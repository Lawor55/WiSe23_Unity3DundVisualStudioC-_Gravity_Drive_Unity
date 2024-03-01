using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //the default 100% of fuel for the player
    public static float fuel = 100f;
    [Header("Fuel Settings:")]
    //the speed at which the player looses fuel
    [SerializeField] [Range(-10f, 100f)] float fuelConsumptionPerSecond;
    private Slider fuelSlider;

    [Header("Map settings")]
    [SerializeField] private float mapSpeed;

    [System.Serializable]
    private class LevelModule
    {
        public GameObject modulePrefab;
        public int ammountToAdd;
    }
    [Header("Level Module Settings:")]
    //all possible LevelModules and their chances
    [SerializeField] private float mostLeftXPossible;
    [SerializeField] private float mostRightXPossible;
    [Header("Modules only get loaded at the start of the Game. Can not be modified at runtime.")]
    [SerializeField] private LevelModule[] levelModules;
    //the names of each LevelModule multiplied with their chance
    private List<string> levelModuleNames = new();
    //one instance of each possible levelModule
    private List<GameObject> levelModulesInScene = new();
    private GameObject mapParent;
    private GameObject currentLevelModule;
    private GameObject lastLevelModule;
    public static bool gameOver = false;
    //[SerializeField] AnimationCurve gameSpeed;
    public static float gameSpeed;


    // Start is called before the first frame update
    void Start()
    {
        fuelSlider = GameObject.Find("UI_Canvas").GetComponentInChildren<Slider>();
        //create a Parent object for all levelModules so its easier to understand hirachy
        mapParent = new GameObject();
        mapParent.name = "Map_Parent";

        foreach (var levelModule in levelModules)
        {
            for (int i = 0; i < levelModule.ammountToAdd; i++)
            {
                //add all levelModule names to a list multiplied by their ammount to create the "lottery system"
                levelModuleNames.Add(levelModule.modulePrefab.name + "(Clone)");
            }
            //instantiate the object pool of levelModules inside of the scene
            GameObject gO = Instantiate(levelModule.modulePrefab, mapParent.transform);
            levelModulesInScene.Add(gO);
            gO.SetActive(false);
        }
        //enable a first randomly selected levelModule so theres no null error in Update when trying to move
        GetNextLevelModule().SetActive(true);
        currentLevelModule.transform.position = new Vector3(mostRightXPossible, mapParent.transform.position.y, mapParent.transform.position.z);
        //print($"Selected next Level Module name: {GetNextLevelModule()}");
    }

    // Update is called once per frame
    void Update()
    {
        //slowly reduce the fuel over time
        fuel -= fuelConsumptionPerSecond * Time.deltaTime;
        fuel = Mathf.Clamp(fuel, 0, 100);
        fuelSlider.value = fuel;
        if (fuel <=0 && !gameOver)
        {
            GameOver();
        }

        //if the active LevelModule reaches its end position choose a new levelModule and enable it on the start position
        if (currentLevelModule.transform.position.x <= mostLeftXPossible)
        {
            currentLevelModule.SetActive(false);
            GetNextLevelModule().SetActive(true);
            currentLevelModule.transform.position = new Vector3(mostRightXPossible, mapParent.transform.position.y, mapParent.transform.position.z);
        }
        else
        {
            //move the active levelModule over the screen
            currentLevelModule.transform.position = new Vector3(currentLevelModule.transform.position.x - (mapSpeed * Time.deltaTime * gameSpeed), mapParent.transform.position.y, mapParent.transform.position.z);
        }
        GameSpeed();
    }

    private GameObject GetNextLevelModule()
    {
        int randomNumber = Random.Range(0, levelModuleNames.Count - 1);
        //int randomNumber = 1;
        string nameOfNextLevelModule = levelModuleNames[randomNumber];
        if (lastLevelModule != null)
        {
            while (lastLevelModule.name == nameOfNextLevelModule)
            {
                nameOfNextLevelModule = levelModuleNames[randomNumber++];
                //print(randomNumber);
                if (randomNumber >= levelModuleNames.Count)
                {
                    randomNumber = 0;
                }
            }
        }

        foreach (var levelModuleInScene in levelModulesInScene)
        {
            if (levelModuleInScene.name == nameOfNextLevelModule)
            {
                currentLevelModule = levelModuleInScene;
                lastLevelModule = currentLevelModule;
                //print($"Selected next Level Module name: {currentLevelModule.name}");
                return currentLevelModule;
            }
        }
        Debug.LogError("No levelModule found!");
        return null;
    }

    public static void GameOver()
    {
        print("Game Over!");
        Time.timeScale = 0;
        gameOver = true;
    }

    private void GameSpeed()
    {
        gameSpeed = 1 + (ScoreManager.score / 1000);
        print($"Gamespeed: {gameSpeed}\nTime since Start: {Time.realtimeSinceStartup}");
    }
}
