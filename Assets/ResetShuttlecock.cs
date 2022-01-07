using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetShuttlecock : MonoBehaviour
{
    public InputActionReference toggleReference = null;


    public void Awake(){
        toggleReference.action.started += Toggle;
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        // gameObject.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
        gameObject.transform.position = new Vector3(0.72f, 2.54f, 3.59f);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().Sleep();
        // gameObject.GetComponent<Rigidbody>().isKinematic = true;
        // bool isActive = !gameObject.activeSelf;
        // gameObject.SetActive(isActive);
    }
}
