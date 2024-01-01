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
        //InegrateBikeParts();
    }

    private void InegrateBikeParts()
    {
        //chasis.position = transform.forward * -1 * front_chasis_offset.magnitude;
        chasis.position = frontTyre.position + front_chasis_offset;
        rearTyre.position = chasis.position + chasis_rear_offset;
    }


}
