using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Evaluation 
{
    public List<int> GetKeyFrames(List<OptitrackSkeletonState> stateBuffer)
    {
        //List<int> a = new List<int>();
        //return a;
        return new List<int>() { 12354 };
    }
    public int GetHighestBodyPart(List<OptitrackSkeletonState> stateList, int bone_id){
        float max_elbow = 0;
        int frame_number = 0;
        OptitrackPose pose;
        for (Int32 i = 0 ; i < stateList.Count ; i++){
            // the line below takes the y coordinate of LFArm of every frame in the take 
            bool foundPose = stateList[i].BonePoses.TryGetValue(bone_id, out pose);
            float comparison = pose.Position.y;
            if (comparison > max_elbow){
                frame_number = i;
                max_elbow = comparison;
            }
        }
        //Debug.Log(stateList[0].BonePoses.values[0].Position.y.GetType());
        //Debug.Log(stateList[0].BonePoses.values[7].Position.y);
        Debug.Log(max_elbow);
        return frame_number;
    }

    public int GetPreparation(List<OptitrackSkeletonState> stateList){
        int frame_number = GetHighestBodyPart(stateList,7);
        return frame_number;
    }

    public int GetPhase2(List<OptitrackSkeletonState> stateList){
        return 0;
    }
    
    public int GetShuttlecockContact(List<OptitrackSkeletonState> stateList){
        int frame_number = GetHighestBodyPart(stateList,5);
        return frame_number;
    }

    public int GetFollowThrough(List<OptitrackSkeletonState> stateList){
        int frame_number = 0;
        float distance = 99999;
        int right_hand_bone_id = 1;
        int left_waist_bone_id = 2;
        OptitrackPose pose;
        OptitrackPose pose2;
        for (int i = 0; i < stateList.Count; i++){
            bool foundPose = stateList[i].BonePoses.TryGetValue(right_hand_bone_id, out pose);
            bool foundPose2 = stateList[i].BonePoses.TryGetValue(left_waist_bone_id, out pose2);
            Vector3 right_hand = pose.Position;
            Vector3 left_waist = pose.Position;
            float distance_i = Vector3.Distance(right_hand,left_waist);
            if (distance_i < distance){
                frame_number = i;
                distance = distance_i;
            }
            else{
                continue;
            }

        }
        return frame_number;
    }
    


}
