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
    [Header("Canvas")]
    public GameObject TimerCanvas;
    public GameObject ScoreCanvas;
    public GameObject CountdownCanvas;
    [Header("Racket")]
    public GameObject Racket;
    [Header("Left Hand")]
    public GameObject Lefthand = null;
    private StartTimer _timerScript;
    private collideEvent _racketScript;
    enum Difficulty
    {
        Beginner,
        Skilled, 
        Expert,
    }

    public enum GameModes 
    {
        Nothing,
        SmashDefense,
        ServiceDrill
    }
    public GameModes gameMode = GameModes.Nothing;
    Difficulty currentDifficulty  = Difficulty.Beginner;
    public List<Vector3> smashSpeeds = new List<Vector3>{ new Vector3(0f, -4f, 14f), new Vector3(0f, -7f, 17f), new Vector3(0f, -10f, 20f) };
    public void Start(){
        _timerScript = TimerCanvas.GetComponent<StartTimer>();
        _racketScript = Racket.GetComponent<collideEvent>();
        rb = gameObject.GetComponent<Rigidbody>();
        toggleReference.action.started += Toggle;
    }

    public void Update()
    {
        gameObject.layer = 7;
        // if(gameObject.transform.localPosition.y <= 0.1f)
        // {
        //     placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
        // }
        if (gameMode == GameModes.Nothing)
        {
            OnClickExitGameMode();
        }
        // OVRInput.Update();
        if (gameMode == GameModes.ServiceDrill){
            // hold shuttlecock
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch) != 0)
            {  
                // to make sure shuttlecock does not collide when being held.
                gameObject.layer = 0;
                gameObject.transform.position = Lefthand.transform.position + new Vector3(0.0f, 0.0f, -0.2f);
            }
            else
            {
                gameObject.layer = 7;
            }
        }
        
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        Destroy(GameObject.FindWithTag("Arrow"));
        if (gameMode == GameModes.SmashDefense)
        {
            float shuttlecockXPos = Random.Range(-0.45f, 3.00f);
            StartCoroutine(smashCoroutine(new Vector3(shuttlecockXPos, 1.977f, -0.212f)));
        }
        else{
            placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
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
         if (gameMode != GameModes.SmashDefense){
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

    public void OnClickEnterSmashDefenseMode()
    {
        TimerCanvas.SetActive(true);
        ScoreCanvas.SetActive(true);
        CountdownCanvas.SetActive(true);
        gameObject.SetActive(true);
        gameMode = GameModes.SmashDefense;
        _timerScript.timeToDisplay = 20;
        _racketScript.scoreAmount = 0;  

        StartCoroutine(smashCoroutine(new Vector3(1.106f, 1.977f, -0.212f)));
    }

    public void OnClickSmashBeginner()
    {
        currentDifficulty = Difficulty.Beginner;
    }
    public void OnClickSmashSkilled()
    {
        currentDifficulty = Difficulty.Skilled;
    }
    public void OnClickSmashExpert()
    {
        currentDifficulty = Difficulty.Expert;
    }
    public void OnClickExitGameMode()
    {
        TimerCanvas.SetActive(false);
        ScoreCanvas.SetActive(false);
        CountdownCanvas.SetActive(false);
        gameMode = GameModes.Nothing;
        
    }
    public void OnClickEnterServiceDrill()
    {
        TimerCanvas.SetActive(true);
        ScoreCanvas.SetActive(true);
        CountdownCanvas.SetActive(true);
        gameObject.SetActive(true);
        gameMode = GameModes.ServiceDrill;
        _timerScript.timeToDisplay = 20;
        _racketScript.scoreAmount = 0;  
    }
}
