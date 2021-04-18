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

    public Transform attackPosition;
    public LayerMask enemyLayer;

    [SerializeField] public PlayerSword playerSword;

    private void Update()
    {
        if (timeBeforeNextAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && playerSword.hasSword)
            {
                Collider2D[] allEnemiesHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                for (int x = 0; x < allEnemiesHit.Length; x++)
                    allEnemiesHit[x].GetComponent<EnemyController>().TakeDamage(damageDealt);
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
