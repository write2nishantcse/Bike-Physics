using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float followDistance = 10f;
    public float followHeight = 2f;
    public float followAngle = 0f;

    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position - target.forward * followDistance + target.up * followHeight;

        // Calculate the rotation of the camera
        Quaternion desiredRotation = Quaternion.LookRotation(-target.forward);

        // Move the camera towards the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.LookAt(target.transform);
    }
}
