using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float smoothSpeed = 0.125f; // The speed at which the camera follows the target
    public Vector3 offset; // The position offset from the target

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate the camera's position towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}