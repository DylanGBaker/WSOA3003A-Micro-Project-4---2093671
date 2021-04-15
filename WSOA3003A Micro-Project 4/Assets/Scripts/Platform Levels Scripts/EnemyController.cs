using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public KeyCode shoot;

    private float xDistance, yDistance, Distance;

    [SerializeField] public PlayerController playerController;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalculateDistanceFromPlayer();

        if (Distance < 100f && Input.GetKeyDown(shoot))
            Attack();

        Debug.Log(Distance);
    }

    public void Attack()
    {
        Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
    }

    public void CalculateDistanceFromPlayer()
    {
        xDistance = playerController.rb.transform.position.x - transform.position.x;
        yDistance = playerController.rb.transform.position.y - transform.position.y;
        Distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
    }
}
