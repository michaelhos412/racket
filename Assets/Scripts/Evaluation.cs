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
    public int GetHighestBodyPart(List<OptitrackSkeletonState> stateList, Int32 bone_id){
        float max_elbow = 0;
        int frame_number = 0;
        //OptitrackPose pose2;
        //"BoneIdToParentIdMap":{"keys":[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,18,19,20,17,21],"values":[0,1,2,3,4,3,6,7,8,3,10,11,12,1,14,15,1,18,19,16,20]}
        //Dictionary<int, int> BoneIdToParentIdMap;
        for (Int32 i = 0 ; i < stateList.Count ; i++){
            OptitrackPose pose;
            
            // the line below takes the y coordinate of LFArm of every frame in the take 
            bool foundPose = stateList[i].BonePoses.TryGetValue(bone_id, out pose);
            //bool foundPose2 = stateList[i].LocalBonePoses.TryGetValue(bone_id, out pose2);
            float comparison = pose.Position.y;
            Debug.Log(foundPose + " " + pose.Position.x + " " + pose.Position.y + " " + pose.Position.z);
            // float comparison2 = pose2.Position.y;
            if (comparison > max_elbow){
                Debug.Log("larger found" + comparison);
                frame_number = i;
                max_elbow = comparison;
            }
            // if(comparison != comparison2){
            //     Debug.Log(bone_id);
            //     break;
            // }
            
        }
        //Debug.Log(stateList[0].BonePoses.values[0].Position.y.GetType());
        //Debug.Log(stateList[0].BonePoses.values[7].Position.y);
        //Debug.Log(max_elbow);
        return frame_number;
    }

    public Vector3 GetGlobalValue(List<OptitrackSkeletonState> stateList, SerializableDictionary<Int32, Int32> BoneIdToParentIdMap, Int32 bone_id, int frame_number){
        OptitrackPose pose;
        if (BoneIdToParentIdMap[bone_id] == 0){
            bool foundPose = stateList[frame_number].LocalBonePoses.TryGetValue(bone_id, out pose);
            return  pose.Position;
        }
        else{
            bool foundPose = stateList[frame_number].LocalBonePoses.TryGetValue(bone_id, out pose);
            return pose.Position + GetGlobalValue(stateList,BoneIdToParentIdMap,BoneIdToParentIdMap[bone_id],frame_number);
        }
    }

    // public int GetPreparation(List<OptitrackSkeletonState> stateList){
    //     int frame_number = GetHighestBodyPart(stateList,7);
    //     return frame_number;
    // }

    // public int GetPhase2(List<OptitrackSkeletonState> stateList){
    //     return 0;
    // }
    
    // public int GetShuttlecockContact(List<OptitrackSkeletonState> stateList){
    //     int frame_number = GetHighestBodyPart(stateList,5);
    //     return frame_number;
    // }

    // public int GetFollowThrough(List<OptitrackSkeletonState> stateList){
    //     int frame_number = 0;
    //     float distance = 99999;
    //     int right_hand_bone_id = 1;
    //     int left_waist_bone_id = 2;
    //     OptitrackPose pose;
    //     OptitrackPose pose2;
    //     for (int i = 0; i < stateList.Count; i++){
    //         bool foundPose = stateList[i].BonePoses.TryGetValue(right_hand_bone_id, out pose);
    //         bool foundPose2 = stateList[i].BonePoses.TryGetValue(left_waist_bone_id, out pose2);
    //         Vector3 right_hand = pose.Position;
    //         Vector3 left_waist = pose.Position;
    //         float distance_i = Vector3.Distance(right_hand,left_waist);
    //         if (distance_i < distance){
    //             frame_number = i;
    //             distance = distance_i;
    //         }
    //         else{
    //             continue;
    //         }

    //     }
    //     return frame_number;
    // }



}
