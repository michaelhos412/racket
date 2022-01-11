using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTester : MonoBehaviour
{
    public OptitrackSkeletonAnimator bot1;
    public OptitrackSkeletonAnimator bot2;
    //bot1.saveLoadManager.skeletonStateBuffer.stateList
    private Evaluation evaluation = new Evaluation();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bot1.SetSkeletonFrame(600);
            bot2.SetSkeletonFrame(52);
            //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].BonePoses.TryGetValue();
            Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].LocalBonePoses);
            Debug.Log(evaluation.GetHighestLeftElbow(bot1.saveLoadManager.skeletonStateBuffer.stateList));
        }
    }
}
