using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float ZeroConstant;

    public Rigidbody2D rb;
    private Scene scene;

    [SerializeField] public PlayerController playerController;
    [SerializeField] public EnemyPatrolAI enemyPatrolAI;
    void Start()
    {
        rb.velocity = new Vector2(speed * enemyPatrolAI.patrolSpeed * Time.fixedDeltaTime, ZeroConstant);
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
            Destroy(gameObject);
        }
    }
}
