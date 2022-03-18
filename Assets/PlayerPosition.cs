using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CenterEyeAnchor;
    void Start()
    {
        Debug.Log(CenterEyeAnchor.transform.position);
        Debug.Log(gameObject.transform.position);
        Debug.Log(CenterEyeAnchor.transform.position - gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.transform.position = CenterEyeAnchor.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z);
    }
}
