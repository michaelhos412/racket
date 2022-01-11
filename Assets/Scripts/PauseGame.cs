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
    }
}
