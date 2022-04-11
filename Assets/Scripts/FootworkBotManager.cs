using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootworkBotManager : MonoBehaviour
{

    Animator animFwdR;
    Animation _animFwdR;
    Animator animFwdL;
    Animation _animFwdL;
    Animator animBwd;
    Animation _animBwd;

    [Header("Shuttle")]
    public GameObject Shuttlecock = null;

    [Header("Bots")]
    public GameObject FwdStepR_FootworkDrill = null;
    public GameObject FwdStepL_FootworkDrill = null;
    public GameObject BwdStep_FootworkDrill = null;
    private ResetShuttlecock _shuttlecockScript = null;
    // Start is called before the first frame update
    void Start()
    {
        animFwdR = FwdStepR_FootworkDrill.GetComponent<Animator>();
        animFwdL = FwdStepL_FootworkDrill.GetComponent<Animator>();
        animBwd = BwdStep_FootworkDrill.GetComponent<Animator>();
        _shuttlecockScript = Shuttlecock.GetComponent<ResetShuttlecock>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Start the tutorial after the Y button is pressed when on footwork drill mode
        if (_shuttlecockScript.gameMode == ResetShuttlecock.GameModes.FootworkDrill)
        {
            if(OVRInput.Get(OVRInput.RawButton.Y))
            {
                Time.timeScale = 0;
                FwdStepR_FootworkDrill.SetActive(true);
                animFwdR.Play("Base Layer.FwdStepR", 0, 0);
            }

            if (animFwdR.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                FwdStepR_FootworkDrill.SetActive(false);
                FwdStepL_FootworkDrill.SetActive(true);
                animFwdL.Play("Base Layer.FwdStepL", 0, 0);
            }

            if (animFwdL.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                FwdStepL_FootworkDrill.SetActive(false);
                BwdStep_FootworkDrill.SetActive(true);
                animBwd.Play("Base Layer.BwdStep", 0, 0);
            }

            if (animBwd.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                BwdStep_FootworkDrill.SetActive(false);
                Time.timeScale = 1;
            }
        }
        
    }
}
