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
    private collideEvent _racketScript;

    void Start()
     {
         _racketScript = Racket.GetComponent<collideEvent>();
     }


    public void Awake(){
        toggleReference.action.started += Toggle;
    }

    public void onDestroy(){
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context){
        gameObject.SetActive(true);
        Racket.SetActive(false);
        RightHand.SetActive(true);
        LaserPointer.SetActive(true);
        MainMenu.SetActive(true);
        PracticeMenu.SetActive(false);
        timer.SetActive(false);
        score.SetActive(false);
        _racketScript.scoreAmount = 0;
        _racketScript.timeToDisplay = 120;

    }
}
