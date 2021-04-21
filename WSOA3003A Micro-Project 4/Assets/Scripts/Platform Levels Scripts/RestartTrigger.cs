using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTrigger : MonoBehaviour
{

    [SerializeField] public PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.transform.position = new Vector2(playerController.RestartPosForLevelZero.transform.position.x, playerController.RestartPosForLevelZero.transform.position.y);
    }
}
