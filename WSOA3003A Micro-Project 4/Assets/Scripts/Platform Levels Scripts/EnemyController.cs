using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int health;
    public bool hasDied;

    public Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        hasDied = false;
    }

    private void Update()
    {
        if (hasDied)
        {
            Die();
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
            hasDied = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
