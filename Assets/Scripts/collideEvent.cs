using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collideEvent : MonoBehaviour
{
    public List<Vector3> racketPositions = new List<Vector3>();
    public int positionTrackingDepthInFrames = 10;
    public float speedMultiplier = 550f; 

    public float normalInfluence = 0.05f; 

    void Update()
    {
        racketPositions.Insert(0, transform.position);
        if (racketPositions.Count > positionTrackingDepthInFrames)
            racketPositions.RemoveAt(positionTrackingDepthInFrames);
    }

    public GameObject ball;
    public Rigidbody myRacketBody;

    private Rigidbody ballBody;

    void Start()
    {
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.Sleep();
    }

  
    void OnCollisionEnter(Collision collision) {
        ball.GetComponent<Rigidbody>().useGravity = true;

        int index = Mathf.Min(racketPositions.Count - 1, positionTrackingDepthInFrames - 1);
        if (index < 0)
            return;
        Vector3 racketVelocity =
            (transform.position - racketPositions[index]) /
            (float) positionTrackingDepthInFrames; 
        racketVelocity *= speedMultiplier;


        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        Vector3 racketNormal = transform.forward;
        if (Vector3.Dot(racketVelocity, racketNormal) < 0)
        {
            racketNormal *= -1;
        }

        racketNormal = racketNormal.normalized;

        ballBody.velocity = racketVelocity + racketNormal * racketVelocity.magnitude * normalInfluence;
        
    }
}