using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionDoors : MonoBehaviour
{
    private Scene scene;
    public int LevelZero = 0;
    public int LevelOne = 1;
    public int LevelTwo = 2;

    [SerializeField] public EnemyController enemyController;


    private void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 1)
            this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (scene.buildIndex == 1 && enemyController.health <= 0)
        {
            this.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && scene.buildIndex == LevelZero)
        {
            collision.attachedRigidbody.velocity = new Vector2(0f, 0f);
            SceneManager.LoadScene(LevelOne);
        } 
        else if (collision.tag == "Player" && scene.buildIndex == LevelOne)
        {
            collision.attachedRigidbody.velocity = new Vector2(0f, 0f);
            SceneManager.LoadScene(LevelTwo);
        }
    }
}
