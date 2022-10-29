using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerAvatarObject;

    public Vector3 CameraOffset;

    public void SetFollowObject(Transform player)
    {
        playerAvatarObject = player;
    }

    void Start()
    {
        CameraOffset = transform.position - playerAvatarObject.position;
    }

    void LateUpdate()
    {
        transform.position = playerAvatarObject.position + CameraOffset;
    }
}
