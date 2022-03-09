using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTester : MonoBehaviour
{
    public OptitrackSkeletonAnimator bot1;
    public OptitrackSkeletonAnimator bot2;
    private SkeletonSaveLoadManager saveLoadManager = new SkeletonSaveLoadManager();
    private OptitrackSkeletonDefinition m_skeletonDef;

    private Evaluation evaluation = new Evaluation();
    //bot1.saveLoadManager.skeletonStateBuffer.stateList
    // Start is called before the first frame update
    void Start()
    {
        m_skeletonDef = saveLoadManager.LoadSkeletonDefinition(Application.streamingAssetsPath + "/SkeletonStates/definition.json");
    }
    // Update is called once per frame
    async void Update()
    {
        // check order of the serialized dictionary with csv
        if (Input.GetKeyDown(KeyCode.A))
        {
            bot1.SetSkeletonFrame(448);                
            Debug.Log(evaluation.GetPreparation(bot1.saveLoadManager.skeletonStateBuffer.stateList));
            // Debug.Log("left foot coordinates");
            // Debug.Log(evaluation.GetCoordinatesBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList,16,448));
            // Debug.Log("right foot coordinates");
            // Debug.Log(evaluation.GetCoordinatesBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList,20,448));
            Debug.Log("Total Score");
            Debug.Log(evaluation.CalculateUserTotalScore(bot1.saveLoadManager.skeletonStateBuffer.stateList));
            Debug.Log("Component Scores");
            Debug.Log(evaluation.Run_Preparation_Tests(bot1.saveLoadManager.skeletonStateBuffer.stateList,evaluation.GetPreparation(bot1.saveLoadManager.skeletonStateBuffer.stateList)));
            Debug.Log(evaluation.Run_ShuttlecockContact_Tests(bot1.saveLoadManager.skeletonStateBuffer.stateList,evaluation.GetShuttlecockContact(bot1.saveLoadManager.skeletonStateBuffer.stateList)));
            Debug.Log(evaluation.Run_FollowThrough_Tests(bot1.saveLoadManager.skeletonStateBuffer.stateList,evaluation.GetFollowThrough(bot1.saveLoadManager.skeletonStateBuffer.stateList)));
        }
    }
}

//    private Evaluation evaluation = new Evaluation();