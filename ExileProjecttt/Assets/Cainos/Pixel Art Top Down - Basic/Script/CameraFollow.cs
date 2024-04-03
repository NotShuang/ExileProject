using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The GameObject that the camera should follow
    public float smoothSpeed = 0.125f; // The speed at which the camera will follow the target
    public Vector3 offset; // The offset position from the target

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Smoothly lerp the camera's position toward the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}