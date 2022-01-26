using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetShuttlecock : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    public List<Vector3> positionList = new List<Vector3>();

    private Rigidbody rb;
    private int positionIndex = 0;
    private Vector3 shuttlecockRotation = new Vector3(109f, 0f, 0f);
    public void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
        toggleReference.action.started += Toggle;
    }

    public void Update()
    {
        if(gameObject.transform.localPosition.y <= 0.1f)
        {
            resetShuttlecock();
        }
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        resetShuttlecock();
    }

    private void resetShuttlecock()
    {
        gameObject.transform.position = positionList[positionIndex];
        gameObject.transform.eulerAngles = shuttlecockRotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        positionIndex = (positionIndex + 1) % positionList.Count;
    }
}
