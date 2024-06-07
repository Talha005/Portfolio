using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 PlayerCameraDifference;
    private void Start()
    {
        PlayerCameraDifference = this.transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + PlayerCameraDifference;
    }
}
