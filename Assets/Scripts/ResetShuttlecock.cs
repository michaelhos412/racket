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
    public bool smashDefenseMode = false;
    public GameObject TimerCanvas;
    public GameObject ScoreCanvas;
    public GameObject CountdownCanvas;
    enum Difficulty
    {
        Beginner,
        Skilled, 
        Expert,
    }
    Difficulty currentDifficulty  = Difficulty.Beginner;
    public List<Vector3> smashSpeeds = new List<Vector3>{ new Vector3(0f, -4f, 14f), new Vector3(0f, -7f, 17f), new Vector3(0f, -10f, 20f) };
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
        Destroy(GameObject.FindWithTag("Arrow"));
        if (smashDefenseMode == false){
            placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
        }
        else{
            float shuttlecockXPos = Random.Range(-0.45f, 3.00f);
            StartCoroutine(smashCoroutine(new Vector3(shuttlecockXPos, 1.977f, -0.212f)));
        }
    }

        public void placeShuttlecock(Vector3 pos){
        // gameObject.transform.position = positionList[positionIndex];
        // gameObject.transform.position = new Vector3(1.0196f, 0.403f, 3.406f);
        // gameObject.transform.position = new Vector3(1.06f, 0.307f, 3.639f);
        Instantiate(arrowHelper, pos, Quaternion.identity);
        gameObject.transform.position = pos;
        gameObject.transform.eulerAngles = shuttlecockRotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
     void OnCollisionEnter(Collision collision) {
         if (smashDefenseMode == false){
             footworkDrill.FootworkDrillShuttlecockCollideEvent();
         }
     }

     IEnumerator smashCoroutine(Vector3 pos)
     {
        Instantiate(arrowHelper, pos, Quaternion.identity);
        gameObject.transform.position = pos;
        gameObject.transform.eulerAngles = shuttlecockRotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(2);
        if (currentDifficulty == Difficulty.Beginner){
            rb.velocity = smashSpeeds[0];
        }
        else if (currentDifficulty == Difficulty.Skilled){
            rb.velocity = smashSpeeds[1];
        }
        else if (currentDifficulty == Difficulty.Expert){
            rb.velocity = smashSpeeds[2];
        }
     }

    public void OnClickEnterSmashDefenseMode(){
        TimerCanvas.SetActive(true);
        ScoreCanvas.SetActive(true);
        CountdownCanvas.SetActive(true);
        smashDefenseMode = true;  

        StartCoroutine(smashCoroutine(new Vector3(1.106f, 1.977f, -0.212f)));
    }

    public void OnClickSmashBeginner(){
        currentDifficulty = Difficulty.Beginner;
    }
    public void OnClickSmashSkilled(){
        currentDifficulty = Difficulty.Skilled;
    }
    public void OnClickSmashExpert(){
        currentDifficulty = Difficulty.Expert;
    }
    public void OnClickExitSmashDefenseMode(){
        TimerCanvas.SetActive(false);
        ScoreCanvas.SetActive(false);
        CountdownCanvas.SetActive(false);
        smashDefenseMode = false;
    }
}
