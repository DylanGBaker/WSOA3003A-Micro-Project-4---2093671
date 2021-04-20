using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrolAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask GroundLayer;
    public Collider2D enemyCollider;
    private Scene scene;
    public Transform Player;
    public GameObject projectile;
    public Transform projectileSpawnPoint;

    public bool canPatrol;
    public bool mustFlip;
    public bool canShoot;
    public float patrolSpeed;
    public float groundcheckradius;
    public float attackRange;
    public float ZeroConstant;
    public float speed;

    [SerializeField] public EnemyController enemyController;
    [SerializeField] public PlayerController playerController;

    private void Start()
    {
        canPatrol = true;
        mustFlip = false;
        canShoot = true;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (canPatrol)
            Patrol();

        CalculateDistanceFromPlayer();

        if (CalculateDistanceFromPlayer() <= attackRange)
        {
            if (Player.position.x < transform.position.x && transform.localScale.x > ZeroConstant)
                FlipEnemy();
            else if (Player.position.x > transform.position.x && transform.localScale.x < ZeroConstant)
                FlipEnemy();

            rb.velocity = Vector2.zero;
            canPatrol = false;

            if (canShoot == true)
            StartCoroutine(SpawnBullet());
        }
        else
        {
            canPatrol = true;
        }

        if (scene.buildIndex == 0 || scene.buildIndex == 1)
        {
            canPatrol = false;
        }
    }

    private void FixedUpdate()
    {
        if (canPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, groundcheckradius, GroundLayer);
        }
    }

    public void Patrol()
    {
        if (mustFlip || enemyCollider.IsTouchingLayers(GroundLayer))
            FlipEnemy();

        float xVelocity = patrolSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    /// <summary>
    /// Checking if the enemy hits a wall or is at the end of a platform so that the enemy can flip.
    /// </summary>
    public void FlipEnemy()
    {
        canPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrolSpeed *= -1;
        canPatrol = true;
    }

    public IEnumerator SpawnBullet()
    {
        canShoot = false;
        yield return new WaitForSeconds(2f);
        GameObject newProjectile = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * patrolSpeed * Time.fixedDeltaTime, ZeroConstant);
        canShoot = true;
    }

    /// <summary>
    /// Calculating the distance between the player and the enemy.
    /// </summary>
    /// <returns></returns>

    public float CalculateDistanceFromPlayer()
    {
        float xDistance = Mathf.Abs(Player.position.x - transform.position.x);
        float yDistance = Mathf.Abs(Player.position.y - transform.position.y);
        float Distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
        return Distance;
    }
}
