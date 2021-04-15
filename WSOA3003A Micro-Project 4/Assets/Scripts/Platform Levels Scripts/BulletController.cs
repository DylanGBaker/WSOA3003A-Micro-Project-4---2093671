using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public Scene scene;
    public bool hitPlayer;


    [SerializeField] public PlayerController playerController;
    void Start()
    {
        rb.velocity = Vector2.left * speed;
        scene = SceneManager.GetActiveScene();
    }

    /// <summary>
    /// Checking if the bullet has hit the enemy.
    /// Depending on which level this is on it will do something different.
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && scene.buildIndex == 0)
        {
            collision.transform.position = new Vector2(playerController.RestartPosForLevelZero.transform.position.x, playerController.RestartPosForLevelZero.transform.position.y);
        }
        Destroy(gameObject); 
    }
}
