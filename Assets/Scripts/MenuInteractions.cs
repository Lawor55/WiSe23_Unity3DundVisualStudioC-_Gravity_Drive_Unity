using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteractions : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        print("Restart!");
    }
}
