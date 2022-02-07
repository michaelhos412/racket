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

    public int GetHighestBodyPart(List<OptitrackSkeletonState> stateList, Int32 bone_id, OptitrackSkeletonDefinition skelDef){
        float max_elbow = float.MinValue;
        int frame_number = 0;
        //"BoneIdToParentIdMap":{"keys":[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,18,19,20,17,21],"values":[0,1,2,3,4,3,6,7,8,3,10,11,12,1,14,15,1,18,19,16,20]}
        //Dictionary<int, int> BoneIdToParentIdMap;
        for (int i = 0 ; i < stateList.Count ; i++){
            //for (int j = 0; j < skelDef.Bones.Count; ++i)
            //{
            //    Int32 boneId = skelDef.Bones[j].Id;
            //    Debug.Log("bone name: " + skelDef.Bones[j].Name + " " + boneId);
            //    OptitrackPose bonePose;
            //    bool foundPose = stateList[j].LocalBonePoses.TryGetValue(boneId, out bonePose);
            //    Debug.Log("check: " + bonePose.Position.x + " " + bonePose.Position.y + " " + bonePose.Position.z);
            //}
            OptitrackPose pose2;
            
            // the line below takes the y coordinate of LFArm of every frame in the take 
            //Vector3 bonePos = GetGlobalValue(stateList[i], skelDef, bone_id, new Vector3());
            bool temp = stateList[i].BonePoses.TryGetValue(bone_id, out pose2);
            Vector3 bonePos2 = pose2.Position;
            float comparison = bonePos2.y;
            //Debug.Log("1 " + bonePos.x + " " + bonePos.y + " " + bonePos.z);
            //Debug.Log("2 " + bonePos2.x + " " + bonePos2.y + " " + bonePos2.z);

            if (comparison > max_elbow)
            {
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

    public Vector3 GetGlobalValue(OptitrackSkeletonState state, OptitrackSkeletonDefinition skelDef, Int32 bone_id, Vector3 acc){
        OptitrackPose pose;
        bool foundPose = state.LocalBonePoses.TryGetValue(bone_id, out pose);
        Vector3 pos = pose.Position;
        //Debug.Log("pos : " + pos.x + " " + pos.y + " " + pos.z + " " + bone_id);
        acc += pos;
        Debug.Log("getGlobal : " + bone_id + " curr POS" + pose.Position.x + " " + pose.Position.y + " "+ pose.Position.z + " acc POS" + " " + acc.x + " " + acc.y + " " + acc.z);

        if (bone_id == 1){
            return  acc;
        }
        else{
            
            return GetGlobalValue(state, skelDef, skelDef.BoneIdToParentIdMap[bone_id], acc);
            //Debug.Log(temp.x + " " + temp.y + " " + temp.z);
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

    public int GetHighestBodyPart2(List<OptitrackSkeletonState> stateList, SerializableDictionary<Int32, Int32> BoneIdToParentIdMap, Int32 bone_id)
    {
        float max_elbow = -999999999;
        int frame_number = 0;
        //OptitrackPose pose2;
        //"BoneIdToParentIdMap":{"keys":[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,18,19,20,17,21],"values":[0,1,2,3,4,3,6,7,8,3,10,11,12,1,14,15,1,18,19,16,20]}
        //Dictionary<int, int> BoneIdToParentIdMap;
        for (int i = 0; i < stateList.Count; i++)
        {
            OptitrackPose pose;
            // the line below takes the y coordinate of LFArm of every frame in the take
            bool foundPose = stateList[i].BonePoses.TryGetValue(bone_id, out pose);
            //bool foundPose2 = stateList[i].LocalBonePoses.TryGetValue(bone_id, out pose2);
            Vector3 coordinates = GetGlobalValue2(stateList, BoneIdToParentIdMap, bone_id, i);
            Vector3 comparison_vector = pose.Position;
            float comparison = pose.Position.y;
            //float comparison = pose.Position.y;
            // Debug.Log(foundPose + " " + pose.Position.x + " " + pose.Position.y + " " + pose.Position.z);
            // float comparison2 = pose2.Position.y;
            if (comparison > max_elbow)
            {
                // Debug.Log("larger found" + comparison);
                frame_number = i;
                max_elbow = comparison;
            }
            if (i == 0)
            {
                Debug.Log("LHand at frame 0 y coordinate is: " + comparison.ToString());
                Debug.Log(comparison_vector);
                Debug.Log(comparison);
                Debug.Log(coordinates);
            }
            if (i == 390)
            {
                Debug.Log("LHand at frame 390 y coordinate is: " + comparison.ToString());
                Debug.Log(comparison_vector);
                Debug.Log(comparison);
                Debug.Log(coordinates);
                // Debug.Log(pose.Position);
            }
            if (i == 448)
            {
                Debug.Log("LHand at frame 448 y coordinate is: " + comparison.ToString());
                Debug.Log(comparison_vector);
                Debug.Log(comparison);
                Debug.Log(coordinates);
                // Debug.Log(pose.Position);
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
    public Vector3 GetGlobalValue2(List<OptitrackSkeletonState> stateList, SerializableDictionary<Int32, Int32> BoneIdToParentIdMap, Int32 bone_id, int frame_number)
    {
        OptitrackPose pose;
        if (BoneIdToParentIdMap[bone_id] == 0)
        {
            bool foundPose = stateList[frame_number].LocalBonePoses.TryGetValue(bone_id, out pose);
            return pose.Position;
        }
        else
        {
            bool foundPose = stateList[frame_number].LocalBonePoses.TryGetValue(bone_id, out pose);
            return pose.Position + GetGlobalValue2(stateList, BoneIdToParentIdMap, BoneIdToParentIdMap[bone_id], frame_number);
        }
    }


}