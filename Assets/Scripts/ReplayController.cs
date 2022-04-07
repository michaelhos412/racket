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
        Debug.Log("Spawning and playing 1");
        pauseMenu.SetActive(false);
        AICoachMenuSelection.SetActive(true);
        yBotGameObject.SetActive(true);
        CloseMenu.SetActive(true);
        StartCoroutine(SpawnAndPlayRecording());
    }

    private IEnumerator SpawnAndPlayRecording()
    {
        Debug.Log("Spawning and playing");
        // yBot.ToggleBotVisibility();
        yield return StartCoroutine(yBot.PlayRecording(0));
        // yBot.ToggleBotVisibility();
    }
}