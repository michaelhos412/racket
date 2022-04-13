using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayController : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public OptitrackSkeletonAnimator yBot = null;
    public GameObject AICoachMenuSelection = null;
    public GameObject yBotGameObject = null;
    public GameObject pauseMenu = null;

    public GameObject CloseMenu = null;
    [Header("Arrow")]
    public GameObject arrow = null;
    
    [Header("Canvas")]
    public GameObject timerCanvas = null;
    public GameObject scoreCanvas = null;
    public GameObject countdownCanvas = null;
    [Header("Shuttle")]
    public GameObject shuttlecock = null;
    private ResetShuttlecock _shuttlecockScript = null;

    public void Start(){
        _shuttlecockScript = shuttlecock.GetComponent<ResetShuttlecock>();
    }
    public void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    public void onDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (_shuttlecockScript.gameMode == ResetShuttlecock.GameModes.Nothing)
        {
            Debug.Log("Spawning and playing 1");
            pauseMenu.SetActive(false);
            AICoachMenuSelection.SetActive(true);
            yBotGameObject.SetActive(true);
            CloseMenu.SetActive(true);
            timerCanvas.SetActive(false);
            scoreCanvas.SetActive(false);
            countdownCanvas.SetActive(false);
            shuttlecock.SetActive(false);
            arrow.SetActive(false);
            StartCoroutine(SpawnAndPlayRecording());
        }
    }

    private IEnumerator SpawnAndPlayRecording()
    {
        Debug.Log("Spawning and playing");
        // yBot.ToggleBotVisibility();
        yield return StartCoroutine(yBot.PlayRecording(0));
        // yBot.ToggleBotVisibility();
    }
}