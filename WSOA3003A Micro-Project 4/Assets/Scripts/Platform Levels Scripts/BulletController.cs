using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public Scene scene;

    [SerializeField] public PlayerController playerController;
    void Start()
    {
        rb.velocity = Vector2.left * speed;
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && scene.buildIndex == 0)
        {
            Debug.Log("herro");
            playerController.transform.position = new Vector2(playerController.RestartPosForLevelZero.transform.position.x, playerController.RestartPosForLevelZero.transform.position.y);
        }
        Destroy(gameObject);
    }
}
