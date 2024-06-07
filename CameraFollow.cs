using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float distance = 3;
    public float height = 2;
    public float smoothTime = 0.25f;
    Vector3 currentVelocity;

    void LateUpdate()
    {
        Vector3 target = player.position + (-player.transform.forward * distance);
        target += Vector3.up * height;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
        //transform.LookAt(player);
    }
}
