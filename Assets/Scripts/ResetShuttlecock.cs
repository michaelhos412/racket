using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetShuttlecock : MonoBehaviour
{
    public FootworkDrill footworkDrill;
    public InputActionReference toggleReference = null;
    public GameObject arrowHelper;


    private Rigidbody rb;
    private int positionIndex = 0;
    private Vector3 shuttlecockRotation = new Vector3(109f, 0f, 0f);
    public void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
        toggleReference.action.started += Toggle;
    }

    public void Update()
    {
        // if(gameObject.transform.localPosition.y <= 0.1f)
        // {
        //     placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
        // }
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
    }

        public void placeShuttlecock(Vector3 pos){
        // gameObject.transform.position = positionList[positionIndex];
        // gameObject.transform.position = new Vector3(1.0196f, 0.403f, 3.406f);
        // gameObject.transform.position = new Vector3(1.06f, 0.307f, 3.639f);
        Instantiate(arrowHelper, gameObject.transform.localPosition, Quaternion.identity);
        gameObject.transform.position = pos;
        gameObject.transform.eulerAngles = shuttlecockRotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
     void OnCollisionEnter(Collision collision) {
            footworkDrill.FootworkDrillShuttlecockCollideEvent();
     }
}
