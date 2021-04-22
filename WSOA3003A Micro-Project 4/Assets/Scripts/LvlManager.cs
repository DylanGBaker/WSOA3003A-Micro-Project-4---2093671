using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    public KeyCode menuButton;

    private void Update()
    {
        if (Input.GetKey(menuButton))
            SceneManager.LoadScene(0);
    }

    public void LoadNewScene (string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
