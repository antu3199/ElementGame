using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
