using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform projectileSpawnPoint;

    [SerializeField] public EnemyPatrolAI enemyPatrolAI;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public IEnumerator Attack()
    {
            yield return new WaitForSeconds(3f);
            Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);      
    }
}
