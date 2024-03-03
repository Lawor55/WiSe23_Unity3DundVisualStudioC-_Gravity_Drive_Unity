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
    [Header("Modules only get loaded at the start of the Game. Can not be modified at runtime.")]
    [SerializeField] private LevelModule[] levelModules;
    [SerializeField] private GameObject firstStartObject;
    //[SerializeField] private GameObject secondStartObject;
    //the names of each LevelModule multiplied with their chance
    private List<string> levelModuleNames = new();
    //one instance of each possible levelModule
    private List<GameObject> levelModulesInScene = new();
    private GameObject mapParent;
    //private GameObject currentLevelModule;
    private GameObject lastLevelModule;

    private List<GameObject> activeLevelModules = new();

    // Start is called before the first frame update
    void Start()
    {
        mapParent = new GameObject();
        mapParent.name = "Map_Parent";
        activeLevelModules.Add(firstStartObject);
        //activeLevelModules.Add(secondStartObject);
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
        while (activeLevelModules[activeLevelModules.Count - 1].transform.position.x + (activeLevelModules[activeLevelModules.Count - 1].transform.GetChild(0).transform.localScale.x / 2) < 42)
        {
            GetNextLevelModule();
            activeLevelModules[activeLevelModules.Count - 1].SetActive(true);
            float modulesSizeFromCenter = activeLevelModules[activeLevelModules.Count - 1].transform.GetChild(0).transform.localScale.x / 2;
            activeLevelModules[activeLevelModules.Count - 1].transform.position = new Vector3(modulesSizeFromCenter + 42, mapParent.transform.position.y, mapParent.transform.position.z);
            //print("Added more in Start");
        }
        //currentLevelModule.transform.position = new Vector3(mostRightXPossible, mapParent.transform.position.y, mapParent.transform.position.z);
        //print($"Selected next Level Module name: {GetNextLevelModule()}");
    }

    // Update is called once per frame
    void Update()
    {
        //if the active LevelModule reaches its end position choose a new levelModule and enable it on the start position
        //if (currentLevelModule.transform.position.x <= mostLeftXPossible)
        if (activeLevelModules[0].transform.position.x <= (-activeLevelModules[0].transform.GetChild(0).transform.localScale.x / 2) - 18)
        {
            //currentLevelModule.SetActive(false);
            activeLevelModules[0].SetActive(false);
            activeLevelModules.RemoveAt(0);
        }

        if (activeLevelModules[activeLevelModules.Count - 1].transform.position.x + (activeLevelModules[activeLevelModules.Count - 1].transform.GetChild(0).transform.localScale.x / 2) < 42)
        {
            GetNextLevelModule();
            activeLevelModules[activeLevelModules.Count - 1].SetActive(true);
            //currentLevelModule.transform.position = new Vector3(mostRightXPossible, mapParent.transform.position.y, mapParent.transform.position.z);
            activeLevelModules[activeLevelModules.Count - 1].transform.position = new Vector3((activeLevelModules[activeLevelModules.Count - 1].transform.GetChild(0).transform.localScale.x / 2) + 42, mapParent.transform.position.y, mapParent.transform.position.z);
        }
        else
        {
            //move the active levelModule over the screen
            //currentLevelModule.transform.position = new Vector3(currentLevelModule.transform.position.x - (mapSpeed * Time.deltaTime * GameManager.gameSpeed), mapParent.transform.position.y, mapParent.transform.position.z);
            foreach (var levelModule in activeLevelModules)
            {
                levelModule.transform.position = new Vector3(levelModule.transform.position.x - (mapSpeed * Time.deltaTime * GameManager.gameSpeed), mapParent.transform.position.y, mapParent.transform.position.z);
            }
        }
    }
    private void GetNextLevelModule()
    {
        int randomNumber = Random.Range(0, levelModuleNames.Count - 1);
        //int randomNumber = 1;
        string nameOfNextLevelModule = levelModuleNames[randomNumber];
        bool matchFound;
        do
        {
            matchFound = false;
            foreach (var activeLevelModule in activeLevelModules)
            {
                if (activeLevelModule.name == nameOfNextLevelModule)
                {
                    nameOfNextLevelModule = levelModuleNames[randomNumber++];
                    if (randomNumber >= levelModuleNames.Count)
                    {
                        randomNumber = 0;
                    }
                    matchFound = true;
                }
            }
        } while (matchFound);

        foreach (var levelModuleInScene in levelModulesInScene)
        {
            if (levelModuleInScene.name == nameOfNextLevelModule)
            {
                //currentLevelModule = levelModuleInScene;
                activeLevelModules.Add(levelModuleInScene);
                //lastLevelModule = currentLevelModule;
                lastLevelModule = activeLevelModules[activeLevelModules.Count - 1];
                //print($"Selected next Level Module name: {currentLevelModule.name}");
                return;
            }
        }
        Debug.LogError("No levelModule found!");
        return;
    }
}
