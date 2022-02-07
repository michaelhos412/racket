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
        float max_elbow = float.MinValue;
        int frame_number = 0;
        for (int i = 0 ; i < stateList.Count ; i++){
            OptitrackPose pose;
            bool temp = stateList[i].BonePoses.TryGetValue(bone_id, out pose);
            Vector3 bonePosition = pose.Position;
            float comparison = bonePosition.y;
            if (comparison > max_elbow)
            {
                frame_number = i;
                max_elbow = comparison;
            }
            
        }
        return frame_number;
    }

    // public Vector3 GetGlobalValue(OptitrackSkeletonState state, OptitrackSkeletonDefinition skelDef, Int32 bone_id, Vector3 acc){
    //     OptitrackPose pose;
    //     bool foundPose = state.LocalBonePoses.TryGetValue(bone_id, out pose);
    //     Vector3 pos = pose.Position;
    //     //Debug.Log("pos : " + pos.x + " " + pos.y + " " + pos.z + " " + bone_id);
    //     acc += pos;
    //     Debug.Log("getGlobal : " + bone_id + " curr POS" + pose.Position.x + " " + pose.Position.y + " "+ pose.Position.z + " acc POS" + " " + acc.x + " " + acc.y + " " + acc.z);

    //     if (bone_id == 1){
    //         return  acc;
    //     }
    //     else{
            
    //         return GetGlobalValue(state, skelDef, skelDef.BoneIdToParentIdMap[bone_id], acc);
    //         //Debug.Log(temp.x + " " + temp.y + " " + temp.z);
    //     }
    // }

    public int GetPreparation(List<OptitrackSkeletonState> stateList){
        int frame_number = GetHighestBodyPart(stateList,8); // 8 is the boneID of LFArm
        return frame_number;
    }

    // public int GetPhase2(List<OptitrackSkeletonState> stateList){
    //     return 0;
    // }

    public int GetShuttlecockContact(List<OptitrackSkeletonState> stateList){
        int frame_number = GetHighestBodyPart(stateList,13); // 13 is the boneID of RHand
        return frame_number;
    }

    public int GetFollowThrough(List<OptitrackSkeletonState> stateList){
        int frame_number = 0;
        float distance = float.MaxValue;
        // 13 is the boneID of RHand
        // 14 is the boneID of LThigh
        OptitrackPose pose;
        OptitrackPose pose2;
        for (int i = 0; i < stateList.Count; i++){
            bool foundPose = stateList[i].BonePoses.TryGetValue(13, out pose);
            bool foundPose2 = stateList[i].BonePoses.TryGetValue(14, out pose2);
            Vector3 right_hand = pose.Position;
            Vector3 left_waist = pose2.Position;
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
