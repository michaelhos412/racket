using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collideEvent : MonoBehaviour
{
    int counter = 0;
    public GameObject racket = null;
    // List<Vector3> pos = new List<Vector3>(5);
    // Vector3[] pos = {new Vector3(0.72f, 2.54f, 3.59f), new Vector3(-0.414f, 3.783f, 3.219f), new Vector3(-1.297f, 3.52f, 2.58f), new Vector3(-0.544f, 2.482f, 2.984f), new Vector3(-0.423f, 4.021f, 3.525f)};
    // List<Vector3> posList = new List<Vector3>(pos);
    // private Vector3 velocityBeforePhysicsUpdate;
    // void FixedUpdate()
    // {
    //     // velocity_ = racket.GetComponent<Rigidbody>().velocity;
    // }
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
    public Rigidbody ballBody;
    public Rigidbody myRacketBody;
    void Start()
    {
        ball.GetComponent<Rigidbody>().Sleep();
    }

    // void Update()
    // {
    //     ball.GetComponent<Rigidbody>().Sleep();
    // }
    void OnCollisionEnter(Collision collision) {
        ball.GetComponent<Rigidbody>().useGravity = true;
        // ballBody.isKinematic = false;
        // gameObject.GetComponent<Rigidbody>().WakeUp();
        // Debug.Log(gameObject2.name.ToString());
        // gameObject2.SetActive(false);
        // gameObject.transform.localScale -= gameObject.transform.localScale;

        // gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 10 * velocity);

        // direction_ = Vector3.Reflect(gameObject.GetComponent<Rigidbody>().velocity.normalized, collision.contacts[0].normal);
        // gameObject.GetComponent<Rigidbody>().velocity = direction_ * speed;
        // float addForce = racket.GetComponent<RigidBody>().velocity.magnitude;
        // speed += addForce;

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
            racketNormal *= -1;

        racketNormal = racketNormal.normalized;

        ballBody.velocity = racketVelocity + racketNormal * racketVelocity.magnitude * normalInfluence;
        
        // StartCoroutine(Respawn(3f));
    }
 
    // IEnumerator Respawn(float timeToRespawn) {
    //     counter += 1;
    //     yield return new WaitForSeconds(timeToRespawn);
    //     gameObject.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
    //     gameObject.transform.position = pos[0];
    //     gameObject.GetComponent<Rigidbody>().Sleep();
    // }
}