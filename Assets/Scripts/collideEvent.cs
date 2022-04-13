using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class collideEvent : MonoBehaviour
{
    public List<Vector3> racketPositions = new List<Vector3>();
    public int positionTrackingDepthInFrames = 10;
    public float speedMultiplier = 550f; 
    public float normalInfluence = 0.05f; 
    public Text score;
    public int scoreAmount = 100;
    public GameObject countdown;

    [Header("Audio")]
    public AudioClip shuttlecockHit; 


    void FixedUpdate()
    {
        

        racketPositions.Insert(0, transform.position);
        if (racketPositions.Count > positionTrackingDepthInFrames)
            racketPositions.RemoveAt(positionTrackingDepthInFrames);
    }

    public GameObject ball;
    public Rigidbody myRacketBody;

    private Rigidbody ballBody;
    public Vector3 com;
    //public GameObject comPoint;

    void Start()
    {
        //comPoint.transform.localPosition = com;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.centerOfMass = com;
        ballBody.Sleep();

    }

  
    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collide");
        ball.GetComponent<Rigidbody>().useGravity = true;
        Vector3 ballPosition = ball.transform.position;
        AudioSource.PlayClipAtPoint(shuttlecockHit, ballPosition);


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

        scoreAmount += 100;
        score.text = string.Format("{0:000000}", scoreAmount);
        
        // countdown.SetActive(false);
    }

    public void OnClickResetScore(){
        scoreAmount = 0;
        score.text = string.Format("{0:000000}", scoreAmount);
    }

    
}