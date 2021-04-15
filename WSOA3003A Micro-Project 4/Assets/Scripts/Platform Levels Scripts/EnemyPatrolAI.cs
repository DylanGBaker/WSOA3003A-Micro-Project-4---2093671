using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrolAI : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool canPatrol;
    public bool mustFlip;
    public float patrolSpeed = 5f;
    public float groundcheckradius = 0.1f;


    public Transform groundCheck;
    public LayerMask GroundLayer;
    public Scene scene;

    private void Start()
    {
        canPatrol = true;
        mustFlip = false;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (canPatrol)
            Patrol();

        if (scene.buildIndex == 0)
            canPatrol = false;
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
        if (mustFlip)
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
}
