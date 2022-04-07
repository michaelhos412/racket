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

 
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _anim = gameObject.GetComponent<Animation>();
    }
    void Update()
    {
   
    }

    public void OnClickPlayRecording()
    {
        anim.speed = 1;
    }
    public void OnClickPauseRecording()
    {
        anim.speed = 0;
    }
    public void OnClickSetState(float desiredTime)
    {
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
}
