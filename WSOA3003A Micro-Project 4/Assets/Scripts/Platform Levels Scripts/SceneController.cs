using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.buildIndex);
    }
}
