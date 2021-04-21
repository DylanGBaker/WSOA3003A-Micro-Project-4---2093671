using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int health;
    public bool hasDied;
    public int damage;
    public float stunTime;
    public float startStunTime = 4f;

    public Rigidbody2D rb;
    public GameObject healthPickups;

    [SerializeField] public PlayerAttackSystem playerAttackSystem;
    [SerializeField] public EnemyPatrolAI enemyPatrolAI;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        hasDied = false;
    }

    private void Update()
    {
        /*if (stunTime <= 0f)
        {
            enemyPatrolAI.canShoot = true;
            enemyPatrolAI.canPatrol = true;
        }
        else if (stunTime > 0f)
        {
            stunTime -= Time.deltaTime;
            enemyPatrolAI.canPatrol = false;
            enemyPatrolAI.canShoot = false;
        }*/

        if (hasDied)
        {
            StartCoroutine(Die());
            Instantiate(healthPickups, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage (int damage)
    {
        //stunTime = startStunTime;

        health -= damage;

        if (health <= 0)
            hasDied = true;
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
