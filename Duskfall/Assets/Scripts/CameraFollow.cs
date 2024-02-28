using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 velocity;
    public float smoothTime;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    void Update()
    {
        // Calculates the position you want for the camera
        Vector3 desiredPosition = target.position + offset;

        //// Smoothly moves the camera to the position you want
        // Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime * Time.deltaTime);

        //// Sets the cameras position to smoothPosition
        //transform.position = smoothPosition;

        // Clamp the desired position within the defined limits
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Set the camera's position to the clamped position
        transform.position = clampedPosition;
    }


}