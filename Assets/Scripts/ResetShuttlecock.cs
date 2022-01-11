using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetShuttlecock : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    public Vector3 position;

    private Rigidbody rb;

    public void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
        toggleReference.action.started += Toggle;
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        gameObject.transform.position = position;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
