
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleUnity : MonoBehaviour
{
    public Vector3 initialVel;
    public Vector3 newPos;
    public Rigidbody rb;
    Vector3 updateForce;
    public Vector3 TargetOffset;
    public Transform MyParent;

    // Start is called before the first frame update
    void Start()
    {
        //set the initial position of a spawned particle
        //transform.position = Vector3.zero;
        //set initial velocity of the spawned particle
        initialVel = 0.0f * Vector3.one;
        rb = GetComponent<Rigidbody>();
        //rb.velocity = initialVel;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        //create a force field
        newPos = transform.position;//get the particle postion
        updateForce = Vector3.one;
        Vector3 desiredPosition = MyParent.position + (MyParent.forward * TargetOffset.z)+(MyParent.up * TargetOffset.y)+(MyParent.right * TargetOffset.x);
        //Vector3 desireddirection = desiredPosition - transform.position;
        //rb.AddForce(desireddirection.normalized * 5f * desireddirection.magnitude);
        Vector3 nextpos = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 3f);
        rb.MovePosition(nextpos);
        //push the particle witht the force field
        //rb.AddForce(updateForce);

    }
}
