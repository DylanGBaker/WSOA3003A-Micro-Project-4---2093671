using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float playerwalkspeed;
    public float playerjumpforce;
    public float zeroConstant;
    public float increasedGravityfactor;

    public Rigidbody2D rb;
    public Vector2 velocity;
    public KeyCode rightbutton, leftbutton, jumpbutton;
    public LayerMask groundlayer;
    public BoxCollider2D playerBoxcollider;
    public GameObject RestartPosForLevelZero;

    [SerializeField] public EnemyController enemyController;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        increaseGravity();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(rightbutton))
            MoveRight();

        if (Input.GetKey(leftbutton))
            MoveLeft();

        if (isGrounded() && Input.GetKey(jumpbutton))
        {
            Jump();
        }
    }

    /// <summary>
    /// Moving the player left and right.
    /// </summary>
    public void MoveRight()
    {
        float xPos = transform.position.x + playerwalkspeed * Time.deltaTime;
        transform.position = new Vector2(xPos, transform.position.y);
        velocity = Vector2.right * playerwalkspeed;

        if (transform.localScale.x > 0f)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    public void MoveLeft()
    {
        float xPos = transform.position.x - playerwalkspeed * Time.deltaTime;
        transform.position = new Vector2(xPos, transform.position.y);
        velocity = Vector2.left * playerwalkspeed;

        if (transform.localScale.x < 0f)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    /// <summary>
    /// Player jump and checking if grounded.
    /// </summary>

    public void Jump()
    {
        rb.AddForce(new Vector2(zeroConstant, playerjumpforce), ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        float raycastDistance = 0.1f;
        RaycastHit2D grounded = Physics2D.Raycast(playerBoxcollider.bounds.center, Vector2.down, playerBoxcollider.bounds.extents.y + raycastDistance, groundlayer);
        Debug.DrawRay(playerBoxcollider.bounds.center, Vector2.down * (playerBoxcollider.bounds.extents.y + raycastDistance));
        return grounded.collider != null;
    }

    /// <summary>
    /// Increasing the players gravity when they jump to make the jump feel better like in Super Mario Bros.
    /// </summary>
    private void increaseGravity() //Increasing the players gravity to make the jump feel better.
    {
        if (rb.velocity.y < 0f && !isGrounded())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * increasedGravityfactor * Time.deltaTime;
        }
    }

    /// <summary>
    /// Take damage when hit.
    /// </summary>
    /// <param name="damage"></param>

    public void TakeDamage (int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Projectile(Clone)")
            TakeDamage(enemyController.damage);
    }
}
