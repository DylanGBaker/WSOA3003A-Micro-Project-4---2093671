using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public Transform Player;
    public KeyCode shoot;
    public Scene scene;

    private float xDistance, yDistance, Distance;
    public float attackRange = 30f;
    public bool hitPlayer;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        hitPlayer = false;
    }

    private void Update()
    {
        //CalculateDistanceFromPlayer();

        Distance = Vector2.Distance(transform.position, Player.transform.position);

        if (Distance < attackRange)
            StartCoroutine(Attack());

        Debug.Log(Distance);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
    }

    /*public float CalculateDistanceFromPlayer()
    {
        xDistance = Player.transform.position.x - transform.position.x;
        yDistance = Player.transform.position.y - transform.position.y;
        Distance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
        return Distance;
    }*/
}
