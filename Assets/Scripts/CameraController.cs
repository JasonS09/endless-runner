using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraVelocity;
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private float lowCameraLimit = 0;
    [SerializeField] private float highCameraLimit = 15;
    [SerializeField] private float minToFollow = 10;
    [SerializeField] private float offset = 2;
    [SerializeField] private bool lookAtPlayer;
    private float initialYPos;

    private void Start()
    {
        initialYPos = player.position.y;
    }

    private void Update()
    {
        if (lookAtPlayer)
        {
            FollowPlayer();
        }
        else
        {
            SmoothMove();
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offset, transform.position.z);

        if (targetPosition.y >= lowCameraLimit && targetPosition.y <= highCameraLimit)
        {
            transform.position = targetPosition;
        }
    }

    private void SmoothMove()
    {
        if (player.position.y > minToFollow)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, minToFollow, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, smoothTime);
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, initialYPos + offset, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, smoothTime);
        }

    }
}
