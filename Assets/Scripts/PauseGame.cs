using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public GameObject Racket = null;
    public GameObject RightHand = null;
    public GameObject LaserPointer = null;
    public GameObject MainMenu = null;
    public GameObject PracticeMenu = null;
    public GameObject timer = null;
    public GameObject score = null;
    public GameObject Shuttlecock = null;

    [Header("Arrow")]
    public GameObject Arrow = null;

    [Header("Buttons")]
    public GameObject practiceSection = null;
    public GameObject demoSection = null;
    public GameObject smashDifficulty = null;
    private collideEvent _racketScript;
    private ResetShuttlecock _shuttlecockScript;

    void Start()
     {
         _racketScript = Racket.GetComponent<collideEvent>();
         _shuttlecockScript = Shuttlecock.GetComponent<ResetShuttlecock>();
     }


    public void Awake(){
        toggleReference.action.started += Toggle;
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        AudioListener.pause = true;
        Arrow.SetActive(false);
        gameObject.SetActive(true);
        Racket.SetActive(false);
        RightHand.SetActive(true);
        LaserPointer.SetActive(true);
        MainMenu.SetActive(true);
        PracticeMenu.SetActive(false);
        timer.SetActive(false);
        score.SetActive(false);
        practiceSection.SetActive(false);
        demoSection.SetActive(false);
        smashDifficulty.SetActive(false);
        _shuttlecockScript.gameMode = ResetShuttlecock.GameModes.Nothing;
    }
}
