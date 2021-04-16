using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerhealth;
    public float playerwalkspeed;
    public float playerjumpforce;
    public float zeroConstant;
    public float increasedGravityfactor;

    public Rigidbody2D rb;
    public Vector2 velocity;
    public KeyCode rightbutton, leftbutton, jumpbutton;
    public LayerMask groundlayer;
    public CapsuleCollider2D playerCapsulecollider;
    public GameObject RestartPosForLevelZero;

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
    }

    public void MoveLeft()
    {
        float xPos = transform.position.x - playerwalkspeed * Time.deltaTime;
        transform.position = new Vector2(xPos, transform.position.y);
        velocity = Vector2.left * playerwalkspeed;
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
        float raycastDistance = 0.01f;
        RaycastHit2D grounded = Physics2D.Raycast(playerCapsulecollider.bounds.center, Vector2.down, playerCapsulecollider.bounds.extents.y + raycastDistance, groundlayer);
        Debug.DrawRay(playerCapsulecollider.bounds.center, Vector2.down * (playerCapsulecollider.bounds.extents.y + raycastDistance));
        return grounded.collider != null;
    }

    private void increaseGravity() //Increasing the players gravity to make the jump feel better.
    {
        if (rb.velocity.y < 0f && !isGrounded())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * increasedGravityfactor * Time.deltaTime;
        }
    }
}
