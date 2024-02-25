using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
