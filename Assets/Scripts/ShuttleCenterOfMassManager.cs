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
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMass, 0.001f);
    // }
}
