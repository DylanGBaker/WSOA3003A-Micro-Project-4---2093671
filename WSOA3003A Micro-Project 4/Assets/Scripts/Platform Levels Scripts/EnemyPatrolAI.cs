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

    public bool canPatrol;
    public bool mustFlip;
    public bool canShoot;
    public float patrolSpeed ;
    public float groundcheckradius;
    private float xDistance, yDistance, Distance;
    public float attackRange;

    [SerializeField] public EnemyController enemyController;

    private void Start()
    {
        canPatrol = true;
        mustFlip = false;
        canShoot = false;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (canPatrol)
            Patrol();

        CalculateDistanceFromPlayer();

        if (CalculateDistanceFromPlayer() <= attackRange)
        {
            if (Player.transform.position.x < transform.position.x && transform.localScale.x > 0f || Player.transform.position.x > transform.position.x && transform.localScale.x < 0f)
                flipEnemy();

            rb.velocity = Vector2.zero;
            canPatrol = false;
            canShoot = true;
        }
        else
        {
            canPatrol = true;
            canShoot = false;
        }

        if (scene.buildIndex == 0)
            canPatrol = false;

        Attack();

        Debug.Log(CalculateDistanceFromPlayer());
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
            flipEnemy();

        float xVelocity = patrolSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void flipEnemy()
    {
        canPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrolSpeed *= -1;
        canPatrol = true;
    }
    public float CalculateDistanceFromPlayer()
    {
        xDistance = Player.transform.position.x - transform.position.x;
        yDistance = Player.transform.position.y - transform.position.y;
        Distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
        return Distance;
    }

    public void Attack()
    {
        if (canShoot)
        {
            Debug.Log("can shoot");
            Instantiate(enemyController.projectile, enemyController.projectileSpawnPoint.position, Quaternion.identity);
            StartCoroutine(AttackTimer());
        }
    }

    public IEnumerator AttackTimer()
    {
        Attack();
        yield return new WaitForSeconds(5f);
        //Attack();
    }
}
