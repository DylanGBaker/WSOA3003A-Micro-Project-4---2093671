using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerhealth = 20;
    public float playerwalkspeed = 5f;
    public float playerjumpforce = 1.5f;
    public float zeroConstant = 0f;

    public Rigidbody2D rb;
    public Vector2 velocity;
    public KeyCode rightbutton, leftbutton, jumpbutton;
    public LayerMask groundlayer;
    public CapsuleCollider2D playerCapsulecollider;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
            Debug.Log("jumping");
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

    private bool isGrounded() //Checking if player is grounded so that they cant jump while in the air.
    {
        float raycastDistance = 0.01f;
        RaycastHit2D grounded = Physics2D.Raycast(playerCapsulecollider.bounds.center, Vector2.down, playerCapsulecollider.bounds.extents.y + raycastDistance, groundlayer);
        Debug.DrawRay(playerCapsulecollider.bounds.center, Vector2.down * (playerCapsulecollider.bounds.extents.y + raycastDistance));
        return grounded.collider != null;
    }
}
