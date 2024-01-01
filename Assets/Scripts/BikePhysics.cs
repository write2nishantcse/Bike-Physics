using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikePhysics : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    [Header("Bike Body")]
    public Transform frontTyre;
    public Transform chasis;
    public Transform rearTyre;
    public float distanceFromGround = 0.5f;
    public float comingDownSpeed = 1;

    private Vector3 front_chasis_offset;
    private Vector3 chasis_rear_offset;

    // Start is called before the first frame update
    void Start()
    {
        front_chasis_offset = chasis.position - frontTyre.position;
        chasis_rear_offset = rearTyre.position - chasis.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for forward/backward and turning
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Lean based on turning
        transform.Rotate(Vector3.up, turnSpeed * horizontal);
        transform.position += transform.forward * vertical;

        // check and move tyre
        CheckTyrePositionWithGround();


        //InegrateBikeParts();
    }

    private void CheckTyrePositionWithGround()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out var castInfo, distanceFromGround + 1))
        {
            transform.position = castInfo.point + castInfo.normal * distanceFromGround;
        }
        else
        {
            transform.position += Vector3.down * comingDownSpeed * Time.deltaTime;
        }
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

    private void InegrateBikeParts()
    {
        //chasis.position = transform.forward * -1 * front_chasis_offset.magnitude;
        chasis.position = frontTyre.position + front_chasis_offset;
        rearTyre.position = chasis.position + chasis_rear_offset;
    }


}
