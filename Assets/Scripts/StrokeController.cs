using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Animation _anim;
    private int currentState = 0;
    // moveCode 1 -> Backhand
    // moveCode 2 -> Serve
    // moveCode 3 -> Forward Step
    // moveCode 4 -> Backward Step
    public int moveCode = 0;
    private Vector3 initialRootPosition;
    private Vector3 initialPosition;
    private Quaternion initialRootRotation;
    private Quaternion initialRotation;
    private string animationName = "";
    private bool isAnimationReversed = false;
    private float desiredTime;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _anim = gameObject.GetComponent<Animation>();
        initialPosition = anim.bodyPosition;
        initialRotation = anim.bodyRotation;
        initialRootPosition = anim.rootPosition;
        initialRootRotation = anim.rootRotation;
        Debug.Log(initialPosition.x + " " +initialPosition.y +" " + initialPosition.z );
    }
    void Update()
    {
        // if (animationName == ""){
        //     return;
        // }
        // if (isAnimationReversed == false){
        //     anim.speed = 1;
        //     anim.Play(animationName, 0, desiredTime);
            
        // }
    }

    public void OnClickPlayRecording()
    {
        OnClickSetState(0f);
        anim.speed = 1;
    }
    public void OnClickPauseRecording()
    {
        anim.speed = 0;
    }
    public void OnClickSetState(float desiredTime)
    {
        anim.speed = 0.03f;    
        anim.rootPosition = initialRootPosition;
        anim.rootRotation = initialRootRotation;
        anim.bodyPosition = initialPosition;
        anim.bodyRotation = initialRotation;
        if(moveCode == 1){
            anim.Play("Base Layer.Backhand_receive2_success_001_FINAL", 0, desiredTime);
        }
        else if(moveCode == 2){
            anim.Play("Base Layer.Serve_004_FINAL", 0, desiredTime);
        }
        else if(moveCode == 3){
            anim.Play("Base Layer.FwdL_step_001_FINAL", 0, desiredTime);
        }
        else if(moveCode == 4){
            anim.Play("Base Layer.Bwd_step_002_FINAL", 0, desiredTime);
        }
    }

    public void LoopAnimationSlowMotion(string animation) {

    }
}
