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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Int32 boneId = m_skeletonDef.Bones[8].Id;
            Debug.Log("bone name: " + m_skeletonDef.Bones[8].Name);
            //bot1.SetSkeletonFrame(600);
            Debug.Log(evaluation.GetHighestBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList,boneId));
            // bot1.SetSkeletonFrame(0);
            bot1.SetSkeletonFrame(evaluation.GetHighestBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList,boneId));
            bot2.SetSkeletonFrame(52);
            OptitrackPose pose;
            //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].BonePoses.TryGetValue();
            //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].LocalBonePoses);
            Debug.Log(evaluation.GetGlobalValue(bot1.saveLoadManager.skeletonStateBuffer.stateList,m_skeletonDef.BoneIdToParentIdMap,13,632));
            Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[632].BonePoses.values[12].Position);
            bool foundPose = bot1.saveLoadManager.skeletonStateBuffer.stateList[632].BonePoses.TryGetValue(13, out pose);
            Debug.Log(pose.Position);
        }
    }
}

//    private Evaluation evaluation = new Evaluation();