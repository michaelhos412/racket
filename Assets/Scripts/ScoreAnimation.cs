using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += transform.forward * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
