using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraOffset;
    [SerializeField] public PlayerController playerController;
    private void Update()
    {
        transform.position = new Vector3(playerController.rb.transform.position.x, playerController.rb.transform.position.y, cameraOffset);
    }
}
