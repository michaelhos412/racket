using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetYBotFrame : MonoBehaviour
{
    // Start is called before the first frame update
    public OptitrackSkeletonAnimator skelAnimator;
    public int frameNumber;
    void Start()
    {
        skelAnimator.SetSkeletonFrame(frameNumber);

    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
