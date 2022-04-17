using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetShuttlecock : MonoBehaviour
{
    [Header("Sound Effect")]
    public AudioSource selectClick = null;
    public AudioSource exitClick = null;
    public AudioSource shuttleAppear = null;

    public FootworkDrill footworkDrill;
    public InputActionReference toggleReference = null;
    public GameObject Arrow = null;
    private Rigidbody rb;
    private int positionIndex = 0;
    private Vector3 shuttlecockRotation = new Vector3(122f, 0f, 0f);
    [Header("Canvas")]
    public GameObject TimerCanvas = null;
    public GameObject ScoreCanvas = null;
    public GameObject CountdownCanvas = null;
    [Header("Racket")]
    public GameObject Racket;
    [Header("Left Hand")]
    public GameObject Lefthand = null;
    private StartTimer _timerScript;
    private collideEvent _racketScript;
    [Header("Bots")]
    public GameObject FwdStepR_FootworkDrill = null;
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
        ServiceDrill,
        FootworkDrill,
        NetShotDrill,
    }
    public GameModes gameMode = GameModes.Nothing;
    Difficulty currentDifficulty  = Difficulty.Beginner;
    public List<Vector3> smashSpeeds = new List<Vector3>{ new Vector3(0f, -4f, 14f), new Vector3(0f, -7f, 17f), new Vector3(0f, -10f, 20f) };
    public void Start(){

        selectClick = selectClick.GetComponent<AudioSource>(); 
        exitClick = exitClick.GetComponent<AudioSource>();
        shuttleAppear = shuttleAppear.GetComponent<AudioSource>();

        _timerScript = TimerCanvas.GetComponent<StartTimer>();
        _racketScript = Racket.GetComponent<collideEvent>();
        rb = gameObject.GetComponent<Rigidbody>();
        toggleReference.action.started += Toggle;
    }

    public void Update()
    {
        gameObject.layer = 7;
        if (gameMode == GameModes.Nothing)
        {

        }
        else if (gameMode == GameModes.ServiceDrill)
        {
            // hold shuttlecock
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch) >= 0.4)
            {  
                // to make sure shuttlecock does not collide when being held.
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
                gameObject.transform.eulerAngles = shuttlecockRotation;
                gameObject.layer = 7;
                gameObject.transform.position = Lefthand.transform.position + new Vector3(0.0f, 0.0f, -0.2f);
                rb.useGravity = false;
            }
            else
            {
                gameObject.layer = 7;
                rb.useGravity = true;
            }
        }
        else if (gameMode == GameModes.FootworkDrill)
        {
            if(gameObject.transform.localPosition.y <= 0.1f)
            {
                placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
            }
        }
        else if (gameMode == GameModes.SmashDefense)
        {
            if(gameObject.transform.position.y <= 0.1f)
            {
                Debug.Log("Smash");
                StartCoroutine(smashCoroutine(new Vector3(0f, 1.977f, -0.212f)));
            }
        }
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        if (gameMode == GameModes.SmashDefense)
        {
            float shuttlecockXPos = Random.Range(-0.45f, 3.00f);
            StartCoroutine(smashCoroutine(new Vector3(shuttlecockXPos, 1.977f, -0.212f)));
        }
        else if (gameMode == GameModes.NetShotDrill)
        {
            float shuttlecockXPos = Random.Range(-0.45f, 3.00f);
            placeShuttlecock(new Vector3(shuttlecockXPos, 1.706f, 0.2f));
        }
        else
        {
            Debug.Log(gameMode);
            placeShuttlecock(new Vector3(1.0196f, 0.403f, 3.406f));
        }
    }

    public void placeShuttlecock(Vector3 pos){
        // gameObject.transform.position = positionList[positionIndex];
        // gameObject.transform.position = new Vector3(1.0196f, 0.403f, 3.406f);
        // gameObject.transform.position = new Vector3(1.06f, 0.307f, 3.639f);
        shuttleAppear.Play();
        gameObject.transform.position = pos;
        gameObject.transform.eulerAngles = shuttlecockRotation;
        if (gameMode == GameModes.NetShotDrill)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    void OnCollisionEnter(Collision collision) {
        if (gameMode == GameModes.FootworkDrill){
            footworkDrill.FootworkDrillShuttlecockCollideEvent();
        }
     }

     IEnumerator smashCoroutine(Vector3 pos)
     {
        float shuttlecockXPos = 0f;
        if (currentDifficulty == Difficulty.Beginner)
        {
            shuttlecockXPos = Random.Range(0.8f, 2f);
        }
        else if (currentDifficulty == Difficulty.Skilled)
        {
            shuttlecockXPos = Random.Range(0.2f, 2.4f);
        }
        else if (currentDifficulty == Difficulty.Expert)
        {
            shuttlecockXPos = Random.Range(-0.45f, 3.00f);
        }
        gameObject.transform.position = new Vector3(shuttlecockXPos, 1.977f, -0.212f);
        gameObject.transform.eulerAngles = shuttlecockRotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (currentDifficulty == Difficulty.Beginner){
            yield return new WaitForSeconds(3);
            rb.velocity = new Vector3(0f, -4f, 14f);
        }
        else if (currentDifficulty == Difficulty.Skilled){
            yield return new WaitForSeconds(2.5f);
            float zSpeed = Random.Range(17f, 25f);
            rb.velocity = new Vector3(0f, -7f, zSpeed);
        }
        else if (currentDifficulty == Difficulty.Expert){
            yield return new WaitForSeconds(2);
            float zSpeed = Random.Range(20f, 30f);
            rb.velocity = new Vector3(0f, -10f, zSpeed);
        }
     }

    public void OnClickEnterSmashDefenseMode()
    {
        enterPlayMode();
        gameMode = GameModes.SmashDefense;

        StartCoroutine(smashCoroutine(new Vector3(1.106f, 1.977f, -0.212f)));
    }

    public void OnClickSmashBeginner()
    {
        selectClick.Play();
        currentDifficulty = Difficulty.Beginner;
    }
    public void OnClickSmashSkilled()
    {
        selectClick.Play();
        currentDifficulty = Difficulty.Skilled;
    }
    public void OnClickSmashExpert()
    {
        selectClick.Play();
        currentDifficulty = Difficulty.Expert;
    }
    public void OnClickExitGameMode()
    {
        exitClick.Play();
        TimerCanvas.SetActive(false);
        ScoreCanvas.SetActive(false);
        CountdownCanvas.SetActive(false);
        Arrow.SetActive(false);
        gameMode = GameModes.Nothing;
        
    }
    public void OnClickEnterServiceDrill()
    {
        enterPlayMode();
        gameMode = GameModes.ServiceDrill;
    }
    public void OnClickEnterFootworkDrill()
    {
        enterPlayMode();
        gameMode = GameModes.FootworkDrill;
    }
    public void OnClickEnterNetShotDrill()
    {
        enterPlayMode();
        gameMode = GameModes.NetShotDrill;
    }
    public void enterPlayMode()
    {
        // AudioListener.pause = true;
        selectClick.Play();
        TimerCanvas.SetActive(true);
        ScoreCanvas.SetActive(true);
        CountdownCanvas.SetActive(true);
        gameObject.SetActive(true);
        Arrow.SetActive(true);
        _timerScript.timeToDisplay = 20;
        _racketScript.scoreAmount = 0;  
    }
}
