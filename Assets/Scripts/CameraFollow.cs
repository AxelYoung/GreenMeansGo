using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    public Vector3 offset;

    private Vector3 velocity = new Vector3();

    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        speed = player.GetComponent<PlayerController>().playerSpeed * 0.275f;
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, speed);
        transform.position = smoothPosition;
    }
}
