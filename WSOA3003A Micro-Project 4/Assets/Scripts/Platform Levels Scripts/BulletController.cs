using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{

    public float ZeroConstant;

    public Rigidbody2D rb;
    private Scene scene;

    [SerializeField] public PlayerController playerController;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (transform.position.x < playerController.transform.position.x)
            StartCoroutine(DestroyBulletAfterCertainTime());
    }

    /// <summary>
    /// Checking if the bullet has hit the enemy.
    /// Depending on which level this is on it will do something different.
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player" when scene.buildIndex == 0 || scene.buildIndex == 1:
                collision.transform.position = new Vector2(playerController.RestartPosForLevelZero.transform.position.x, playerController.RestartPosForLevelZero.transform.position.y);
                break;

            default:
                Destroy(gameObject);
                break;
        }
        Destroy(gameObject);
    }

    IEnumerator DestroyBulletAfterCertainTime()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
