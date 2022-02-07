using System.Collections;
using System.Collections.Generic;
using System;
using Unity;
using UnityEngine;
public class Evaluation 
{
    private SkeletonSaveLoadManager saveLoadManager = new SkeletonSaveLoadManager();
    private OptitrackSkeletonDefinition m_skeletonDef;
    

    public List<int> GetKeyFrames(List<OptitrackSkeletonState> stateBuffer)
    {
        //List<int> a = new List<int>();
        //return a;
        return new List<int>() { 12354 };
        
    }
    
    //Statelist array with length (n) number of frames in the take
    //In each entry of statelist 

    public int GetHighestLeftElbow(List<OptitrackSkeletonState> stateList){
        // float max_elbow = 0;
        int frame_number = 0;
        // OptitrackPose pose;
        // for (int i = 0 ; i < stateList.Count ; i++){
        //     // the line below takes the y coordinate of LFArm of every frame in the take 
        //     bool foundPose = stateList[i].BonePoses.TryGetValue(i, out pose);
        //     float comparison = pose.Position.y;
        //     if (comparison > max_elbow){
        //         frame_number = i;
        //         max_elbow = comparison;
        //     }
        //     else{
        //         continue;
        //     }
        //     Debug.Log(comparison);
        // }
        
        // Debug.Log(stateList[0].BonePoses.values[0].Position.y.GetType());
        // Debug.Log(stateList[0].BonePoses.values[8].Position.y);
        // Debug.Log(stateList[1].BonePoses.values[8].Position.y);
        return frame_number;
    }
    public float getGlobalPosition(List<OptitrackSkeletonState> stateList, int time_point, int pivot_index){
        m_skeletonDef = saveLoadManager.LoadSkeletonDefinition(Application.streamingAssetsPath + "/SkeletonStates/definition.json");

        int id = pivot_index;
        int parentId = m_skeletonDef.BoneIdToParentIdMap[id]; 
        float posX = 0.0f; 
        posX = stateList[time_point].LocalBonePoses.values[id].Position.x;
        // Vector3 pos = new Vector3();
        while (parentId != 0){
            posX = posX + stateList[time_point].LocalBonePoses.values[id].Position.x; 
            // pos = pos + stateList[time_point].LocalBonePoses.values[id].Position;
            Debug.Log("posX: " + stateList[time_point].LocalBonePoses.values[id].Position.x); 
            id = parentId; 
            parentId = m_skeletonDef.BoneIdToParentIdMap[parentId];
        }
        Debug.Log("final posX: " + posX); 
        return posX; 
    }

    public float GetJointAngle( List<OptitrackSkeletonState> stateList, int time_point, List<int> pivot_indexes){
    // time_point : frame number to be analyzed
    // pivot_indexes : id of bone as pivot for joint angles

        // float angle = 0.0f; 
        
        // Vector3 a = stateList[time_point].BonePoses.values[pivot_indexes[0]].Position;
        Vector3 a = stateList[time_point].BonePoses.values[pivot_indexes[0]].Position;
        Vector3 b = stateList[time_point].BonePoses.values[pivot_indexes[1]].Position;
        Vector3 c = stateList[time_point].BonePoses.values[pivot_indexes[2]].Position;

        Vector3 d = stateList[time_point].BonePoses.values[20].Position;
        Vector3 u = c-b;
        Vector3 v = a-b;

        float angleBetween = 0.0f; 
        angleBetween = Vector3.Angle(u, v); 
        Debug.Log("Angle: " + angleBetween);
        Debug.Log("d: " + d);

        // Debug.Log("def" + def.BoneIdToParentIdMap);

        // for (int i = 0 ; i < stateList.Count ; i++){


        //     Debug.Log("Bone Pose frame: " + i + ", bone: " + 1 + " is " + stateList[i].BonePoses.values[5].Position.y);
        //     // Debug.Log("Local Bone Pose frame: " + i + ", bone: " + 1+ " is " + f);
        // }
        getGlobalPosition(stateList, time_point, pivot_indexes[0]);
        return angleBetween;

    }

}
