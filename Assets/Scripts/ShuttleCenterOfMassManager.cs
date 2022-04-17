using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleCenterOfMassManager : MonoBehaviour
{
    public Vector3 CenterOfMass;
    protected Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.centerOfMass = CenterOfMass;
        gameObject.transform.up = Vector3.Slerp(gameObject.transform.up, rb.velocity.normalized, Time.deltaTime * 2.3f);
        // gameObject.transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMass, 0.001f);
    // }
}
