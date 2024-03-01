using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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
            currentLevelModule.transform.position = new Vector3(currentLevelModule.transform.position.x - (mapSpeed * Time.deltaTime * GameManager.gameSpeed), mapParent.transform.position.y, mapParent.transform.position.z);
        }
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
}
