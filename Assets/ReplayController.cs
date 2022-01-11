using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayController : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public OptitrackSkeletonAnimator yBot = null;
 

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
        yBot.OnClickPlayRecording();
    }
}