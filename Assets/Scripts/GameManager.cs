using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float fuel = 100f;
    [SerializeField] [Range(-10f,100f)] float fuelConsumptionPerSecond;

    private Slider fuelSlider;

    [System.Serializable]
    private class LevelModule
    {
        public GameObject modulePrefab;
        public int chanceToSpawn;
    }
    [SerializeField] private LevelModule[] levelModules;

    // Start is called before the first frame update
    void Start()
    {
        fuelSlider = GameObject.Find("UI_Canvas").GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelSlider.value -= fuelConsumptionPerSecond * Time.deltaTime;
    }
}
