using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSword : MonoBehaviour
{
    public GameObject parentPlayer;
    public GameObject swordPosition;
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && scene.buildIndex == 1)
        {
            transform.parent = parentPlayer.transform;
            transform.position = swordPosition.transform.position;
        }
    }
}
