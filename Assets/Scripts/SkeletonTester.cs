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
        // check order of the serialized dictionary with csv
        if (Input.GetKeyDown(KeyCode.A))
        {

            for (int i = 0; i < m_skeletonDef.Bones.Count; ++i)
            {
                Int32 boneId = m_skeletonDef.Bones[i].Id;
                Debug.Log("bone name: " + m_skeletonDef.Bones[i].Name + " " + boneId);
                OptitrackPose bonePose;
                OptitrackPose bonePose2;
                bonePose = bot1.saveLoadManager.skeletonStateBuffer.stateList[0].BonePoses[boneId];
                bonePose2 = bot1.saveLoadManager.skeletonStateBuffer.stateList[0].LocalBonePoses[boneId];
                Debug.Log("0: " + bot1.saveLoadManager.skeletonStateBuffer.stateList[0] + " " + i);
                Vector3 globalPos = evaluation.GetGlobalValue(bot1.saveLoadManager.skeletonStateBuffer.stateList[0], m_skeletonDef, boneId, new Vector3());
                Debug.Log("1: bonePose" + bonePose.Position.x + " " + bonePose.Position.y + " " + bonePose.Position.z);
                Debug.Log("2: " + globalPos.x + " " + globalPos.y + " " + globalPos.z);
                Debug.Log("3: localBonePose" + bonePose2.Position.x + " " + bonePose2.Position.y + " " + bonePose2.Position.z);
                Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].BonePoses);
            }

            //bot1.SetSkeletonFrame(600);


                Debug.Log(evaluation.GetHighestBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList, 9, m_skeletonDef ));

                //Debug.Log(evaluation.GetHighestBodyPart2(bot1.saveLoadManager.skeletonStateBuffer.stateList, m_skeletonDef.BoneIdToParentIdMap, boneId));
                // bot1.SetSkeletonFrame(0);
                //bot1.SetSkeletonFrame(evaluation.GetHighestBodyPart(bot1.saveLoadManager.skeletonStateBuffer.stateList,boneId));
                //bot2.SetSkeletonFrame(52);
                //OptitrackPose pose;
                //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].BonePoses.TryGetValue();
                //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[0].LocalBonePoses);
                //Vector3 pos = evaluation.GetGlobalValue(bot1.saveLoadManager.skeletonStateBuffer.stateList[0], m_skeletonDef, boneId);
                //Debug.Log("global pos : " + pos.x + " " + pos.y + " " + pos.z);

                //Debug.Log(bot1.saveLoadManager.skeletonStateBuffer.stateList[632].BonePoses.values[12].Position);
                //bool foundPose = bot1.saveLoadManager.skeletonStateBuffer.stateList[632].BonePoses.TryGetValue(13, out pose);
                //Debug.Log(pose.Position);
        }
    }
}

//    private Evaluation evaluation = new Evaluation();