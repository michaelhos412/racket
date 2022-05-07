using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public FootworkDrill footworkDrill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "PlayerCollider"){
            Debug.Log("player touched base");
            footworkDrill.TouchBase();
        }
        
    }

    // void OnControllerColliderHit(ControllerColliderHit hit){
    //     Debug.Log("player touched base, character");
    //     footworkDrill.TouchBase();
    // }
}
