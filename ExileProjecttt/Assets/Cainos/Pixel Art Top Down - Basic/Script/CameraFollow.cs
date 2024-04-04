using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The GameObject that the camera should follow
    public float smoothSpeed = 0.125f; // The speed at which the camera will follow the target
    public Vector3 offset; // The offset position from the target
    public float zoomSpeed = 2f; // The speed at which the camera zooms in and out
    public float minZoom = 2f; // The minimum zoom distance
    public float maxZoom = 5f; // The maximum zoom distance

    private Camera cam;
    private float currentZoom;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        cam = GetComponent<Camera>();
        currentZoom = cam.orthographicSize;
    }

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

        // Smoothly interpolate the camera's position toward the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Zoom in and out based on the scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        cam.orthographicSize = currentZoom;
    }
}