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
    public Text timeText;
    public Text countdownText;
    public int scoreAmount = 100;
    private int nextUpdate = 1;
    public int timeToDisplay = 120;
    public GameObject countdown;


    void Update()
    {
        score.text = string.Format("{0:000000}", scoreAmount);
        if(Time.time>=nextUpdate){
            Debug.Log(Time.time+">="+nextUpdate);
            // Change the next update (current second+1)
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            // Call your fonction
            UpdateEverySecond();
        }

        racketPositions.Insert(0, transform.position);
        if (racketPositions.Count > positionTrackingDepthInFrames)
            racketPositions.RemoveAt(positionTrackingDepthInFrames);
    }

    public GameObject ball;
    public Rigidbody myRacketBody;

    private Rigidbody ballBody;
    public Vector3 com;
    public GameObject comPoint;
    public GameObject popUpText;


    void Start()
    {
        comPoint.transform.localPosition = com;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.centerOfMass = com;
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

        scoreAmount += 100;
        score.text = string.Format("{0:000000}", scoreAmount);
        
        countdown.SetActive(false);

    }

    void UpdateEverySecond()
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}