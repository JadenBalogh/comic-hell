using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveTime = 0.2f;

    private Vector2 currVel;

    private void FixedUpdate()
    {
        Vector3 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVel, moveTime);
        Vector3 zOffset = Vector3.forward * transform.position.z;
        transform.position = targetPos + zOffset;
    }
}
