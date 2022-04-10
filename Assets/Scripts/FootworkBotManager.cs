using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootworkBotManager : MonoBehaviour
{

    Animator animFwdR;
    Animation _animFwdR;
    public GameObject FwdStepR_FootworkDrill = null;
    // Start is called before the first frame update
    void Start()
    {
        animFwdR = FwdStepR_FootworkDrill.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.Y))
        {
            FwdStepR_FootworkDrill.SetActive(true);
            animFwdR.Play("Base Layer.FwdStepR", 0, 0);
            // FwdStepR_FootworkDrill.SetActive(false);
        }

        if (animFwdR.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            FwdStepR_FootworkDrill.SetActive(false);
        }
    }
}
