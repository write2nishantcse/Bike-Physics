using System;
using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    public Rigidbody rb;
    public ForceMode forceMode;
    public float speed = 10f;
    public float turnSpeed = 50f;
    public float distanceFromGround = 0.5f;

    [Header("Test Variables")]
    [SerializeField]private float sphereGizmoRadius = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input for forward/backward and turning
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Lean based on turning
        var newRot = rb.rotation.eulerAngles + new Vector3(0, horizontal * turnSpeed, 0);
        rb.rotation = Quaternion.Euler(newRot);

        if (!IsTouchingGround())
        {
            return;
        }

        // Apply movement in FixedUpdate for physics
        Vector3 moveDirection = new Vector3(0, 0, vertical).normalized;
        rb.AddForce(transform.forward * speed * vertical, forceMode);
    }

    private bool IsTouchingGround()
    {
        
        if (Physics.Raycast(transform.position, transform.up * -1, out var castInfo))
        {
            if (castInfo.distance <= distanceFromGround)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        var ray = new Ray(transform.position, transform.up * -1);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
        
        if (Physics.Raycast(ray, out var castInfo))
        {
            if (castInfo.distance <= distanceFromGround)
            {
                var spherePos = transform.position + transform.up * -1 * distanceFromGround;
                Gizmos.DrawSphere(spherePos, sphereGizmoRadius);
            }
        }
        
    }
}
