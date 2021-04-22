using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttackSystem : MonoBehaviour
{
    public int damageDealt;
    private float timeBeforeNextAttack;
    public float startTimeBeforeNextAttack;
    public float attackRange;
    public bool hasKilledEnemy;
    public Collider2D[] EnemiesHit;

    public Transform attackPosition;
    public LayerMask enemyLayer;
    public Animator anim;
    private Scene scene;

    [SerializeField] public EnemyPatrolAI enemyPatrolAI;

    private void Start()
    {
        hasKilledEnemy = false;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
            if (timeBeforeNextAttack <= 0)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    anim.SetTrigger("playerAttack");
                    EnemiesHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                    for (int x = 0; x < EnemiesHit.Length; x++)
                    {
                        if (EnemiesHit[x].tag == "BaseEnemy")
                        {
                            EnemiesHit[x].GetComponent<EnemyController>().TakeDamage(damageDealt);
                        }
                    }

                }
                timeBeforeNextAttack = startTimeBeforeNextAttack;
            }
            else
            {
                timeBeforeNextAttack -= Time.deltaTime;
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
