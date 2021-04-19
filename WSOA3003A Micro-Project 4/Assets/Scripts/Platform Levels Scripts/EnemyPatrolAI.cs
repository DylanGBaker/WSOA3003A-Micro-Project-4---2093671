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
    public float patrolSpeed = 50f;
    public float groundcheckradius;
    private float xDistance, yDistance, Distance;
    public float attackRange;
    public float ZeroConstant;
    public float speed = 10f;

    [SerializeField] public EnemyController enemyController;
    [SerializeField] public PlayerController playerController;

    private void Start()
    {
        canPatrol = true;
        mustFlip = false;
        canShoot = true;
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            canPatrol = false;
        }
        else if (scene.buildIndex == 1)
        {
            canPatrol = false;
            canShoot = false;
        }
    }

    private void Update()
    {
        if (canPatrol)
            Patrol();

        CalculateDistanceFromPlayer();

        if (xDistance <= attackRange)
        {
            if (playerController.rb.transform.position.x < transform.position.x && transform.rotation.y == 0f || playerController.rb.transform.position.x > transform.position.x && transform.rotation.y == 180f)
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




        Debug.Log(xDistance);

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

    public void FlipEnemy()
    {
        canPatrol = false;
        transform.Rotate(0f, 180f, 0f);
        patrolSpeed *= -1;
        canPatrol = true;
    }
    public void CalculateDistanceFromPlayer()
    {
        xDistance = Mathf.Abs(transform.position.x - playerController.rb.transform.position.x);
        //yDistance = playerController.rb.transform.position.y - transform.position.y;
        //Distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
    }

    public IEnumerator SpawnBullet()
    {
        canShoot = false;
        yield return new WaitForSeconds(2f);
        GameObject newProjectile = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * patrolSpeed * Time.fixedDeltaTime, ZeroConstant);
        canShoot = true;
    }

}
