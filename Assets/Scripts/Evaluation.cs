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
            //Debug.Log("posX: " + stateList[time_point].LocalBonePoses.values[id].Position.x); 
            id = parentId; 
            parentId = m_skeletonDef.BoneIdToParentIdMap[parentId];
        }
        //Debug.Log("final posX: " + posX); 
        return posX; 
    }

    public float GetJointAngle( List<OptitrackSkeletonState> stateList, int time_point, List<int> pivot_indexes){
    // time_point : frame number to be analyzed
    // pivot_indexes : id of bone as pivot for joint angles
        OptitrackPose pose_a, pose_b, pose_c;

        bool temp_a = stateList[time_point].BonePoses.TryGetValue(pivot_indexes[0], out pose_a);
        bool temp_b = stateList[time_point].BonePoses.TryGetValue(pivot_indexes[1], out pose_b);
        bool temo_c = stateList[time_point].BonePoses.TryGetValue(pivot_indexes[2], out pose_c);
        Vector3 bonePose_a = pose_a.Position;
        Vector3 bonePose_b = pose_b.Position;
        Vector3 bonePose_c = pose_c.Position;

        Vector3 u = bonePose_c - bonePose_b;
        Vector3 v = bonePose_a - bonePose_b; 


        float angleBetween = 0.0f; 
        angleBetween = Vector3.Angle(u, v); 
        //Debug.Log("Angle: " + angleBetween);
        //Debug.Log("d: " + d);

        Debug.Log("Angle: " + angleBetween);
        return angleBetween;

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
    
    public float Preparation_Test1(List<OptitrackSkeletonState> stateList, int frame_number){
        //test 1 left elbow is higher than the right elbow
        //get the Y of left elbow (LFArm)
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(8, out pose);
        Vector3 bonePosition = pose.Position;
        float LElbow = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(12, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RElbow = bonePosition.y;
        if (LElbow > RElbow){
            return 1;
        }
        else{
            return 0;
        }

    }

    public float Preparation_Test2(List<OptitrackSkeletonState> stateList, int frame_number){
        //test 2 left elbow is higher than the left shoulder
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(8, out pose);
        Vector3 bonePosition = pose.Position;
        float LElbow = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(7, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float LShoulder= bonePosition.y;
        if (LElbow > LShoulder){
            return 1;
        }
        else{
            return 0;
        }
    }
    
    public float Preparation_Test3(List<OptitrackSkeletonState> stateList, int frame_number){
        //test 3 right shoulder is higher than right elbow
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(12, out pose);
        Vector3 bonePosition = pose.Position;
        float RElbow = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(11, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RShoulder= bonePosition.y;
        if (RShoulder> RElbow){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float Preparation_Test4(List<OptitrackSkeletonState> stateList, int frame_number){
        //test 4 left foot in front of right foot
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(17, out pose);
        Vector3 bonePosition = pose.Position;
        float LFoot = bonePosition.z;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(21, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RFoot = bonePosition.z;
        if (LFoot < RFoot){
            return 1;
        }
        else{
            return 0;
        }       
    }

    public float Preparation_Test5(List<OptitrackSkeletonState> stateList, int frame_number){
        //test5 collinearity test do this later
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(8, out pose);
        Vector3 LElbow = pose.Position;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(7, out pose2);
        Vector3 LShoulder = pose2.Position;
        OptitrackPose pose3;
        bool temp3 = stateList[frame_number].BonePoses.TryGetValue(12, out pose3);
        Vector3 RElbow = pose3.Position;
        OptitrackPose pose4;
        bool temp4 = stateList[frame_number].BonePoses.TryGetValue(11, out pose4);
        Vector3 RShoulder = pose4.Position;
        Vector3 v1 = Vector3.Normalize(LElbow - LShoulder);
        Vector3 v2 = Vector3.Normalize(LShoulder - RShoulder);
        Vector3 v3 = Vector3.Normalize(RShoulder - RElbow);
        double check_1 = Vector3.Dot(v1,v2);
        double check_2 = Vector3.Dot(v1,v2);
        if (check_1 > 0.8 && check_2 > 0.6){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float Preparation_Test6(List<OptitrackSkeletonState> stateList, int frame_number){
        //tes 6 feet are open wide enough
        List<int> pivot_indexes = new List<int>(new int[] { 15,1,19 } );
        float angle = GetJointAngle(stateList,frame_number,pivot_indexes);
        if ( angle >= 40 && angle <= 60){
            return 1;
        }
        else{
            return 0;
        }

    }

    public float Preparation_Test7(List<OptitrackSkeletonState> stateList, int frame_number){
        //test 7 knees are bent enough or not
        List<int> pivot_indexes_left = new List<int>(new int[] { 14,15,16 } );
        List<int> pivot_indexes_right = new List<int>(new int[] { 18,19,20 } );
        float left_knee = GetJointAngle(stateList,frame_number,pivot_indexes_left);
        float right_knee = GetJointAngle(stateList,frame_number,pivot_indexes_right);
        if (left_knee < 165 && right_knee < 165){
            return 1;
        }
        else{
            return 0;
        }
    }
    
    public double Run_Preparation_Tests(List<OptitrackSkeletonState> stateList, int frame_number){
        //final weighted score for preparation
        double t1 = Preparation_Test1(stateList,frame_number) * 0.05;
        double t2 = Preparation_Test2(stateList,frame_number) * 0.10;
        double t3 = Preparation_Test3(stateList,frame_number) * 0.05;
        double t4 = Preparation_Test4(stateList,frame_number) * 0.20;
        double t5 = Preparation_Test5(stateList,frame_number) * 0.35;
        double t6 = Preparation_Test6(stateList,frame_number) * 0.10;
        double t7 = Preparation_Test7(stateList,frame_number) * 0.15;
        return t1 + t2 + t3 + t4 + t5 + t6 + t7;
    }
    // public int GetPhase2(List<OptitrackSkeletonState> stateList){
    //     return 0;
    // }

    public int GetShuttlecockContact(List<OptitrackSkeletonState> stateList){
        int frame_number = GetHighestBodyPart(stateList,13); // 13 is the boneID of RHand
        return frame_number;
    }

    public float ShuttlecockContact_test1(List<OptitrackSkeletonState> stateList, int frame_number){
        //armpit joint angle test
        float angle = GetJointAngle(stateList,frame_number,new List<int>(new int[] { 12,11,18 } ));
        if ( angle >= 110 && angle <= 180){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float ShuttlecockContact_test2(List<OptitrackSkeletonState> stateList, int frame_number){
        // left hand lower than the shoulder
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(7, out pose);
        Vector3 bonePosition = pose.Position;
        float LShoulder = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(9, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float LHand = bonePosition.y;
        if (LShoulder > LHand){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float ShuttlecockContact_test3(List<OptitrackSkeletonState> stateList, int frame_number){
        // right elbow higher than shoulder
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(12, out pose);
        Vector3 bonePosition = pose.Position;
        float RElbow = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(11, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RShoulder= bonePosition.y;
        if (RElbow > RShoulder){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float ShuttlecockContact_test4(List<OptitrackSkeletonState> stateList, int frame_number){
        // shuttlecock contact foot in front of preparation foot
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(20, out pose);
        Vector3 bonePosition = pose.Position;
        float RFoot1 = bonePosition.z;
        int preparation_frame_number = GetPreparation(stateList);
        OptitrackPose pose2;
        bool temp2 = stateList[preparation_frame_number].BonePoses.TryGetValue(20, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RFoot2= bonePosition.z;
        if (RFoot1 > RFoot2){
            return 1;
        }
        else{
            return 0;
        }
    }

    public float ShuttlecockContact_test5(List<OptitrackSkeletonState> stateList, int frame_number){
        // right hand above right elbow
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(12, out pose);
        Vector3 bonePosition = pose.Position;
        float RElbow = bonePosition.y;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(13, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RHand = bonePosition.y;
        if (RHand > RElbow){
            return 1;
        }
        else{
            return 0;
        }
    }

    public double Run_ShuttlecockContact_Tests(List<OptitrackSkeletonState> statelist, int frame_number){
        double t1 = ShuttlecockContact_test1(statelist,frame_number) * 0.30;
        double t2 = ShuttlecockContact_test2(statelist,frame_number) * 0.10;
        double t3 = ShuttlecockContact_test3(statelist,frame_number) * 0.35;
        double t4 = ShuttlecockContact_test4(statelist,frame_number) * 0.15;
        double t5 = ShuttlecockContact_test5(statelist,frame_number) * 0.10;
        return t1 + t2 + t3 + t4 + t5;
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

    public float FollowThrough_Test1 (List<OptitrackSkeletonState> stateList, int frame_number){
        //right foot in front of left foot now
        OptitrackPose pose;
        bool temp = stateList[frame_number].BonePoses.TryGetValue(17, out pose);
        Vector3 bonePosition = pose.Position;
        float LFoot = bonePosition.z;
        OptitrackPose pose2;
        bool temp2 = stateList[frame_number].BonePoses.TryGetValue(21, out pose2);
        Vector3 bonePosition2 = pose2.Position;
        float RFoot = bonePosition.z;
        if (RFoot < LFoot){
            return 1;
        }
        else{
            return 0;
        }       
    }

    public double Run_FollowThrough_Tests(List<OptitrackSkeletonState> stateList, int frame_number){
        double t1 = FollowThrough_Test1(stateList,frame_number) * 1.00;
        return t1;
    }

    public double CalculateUserTotalScore(List<OptitrackSkeletonState> stateList){
        double preparation = Run_Preparation_Tests(stateList,GetPreparation(stateList)) * 0.4;
        double shuttlecockcontact = Run_ShuttlecockContact_Tests(stateList,GetShuttlecockContact(stateList)) * 0.4;
        double followthrough = Run_FollowThrough_Tests(stateList,GetFollowThrough(stateList)) * 0.2;
        return preparation + shuttlecockcontact + followthrough;
    }

    public Vector3 GetCoordinatesBodyPart(List<OptitrackSkeletonState> stateList, Int32 bone_id, Int32 frame_number){
        OptitrackPose pose;
        bool foundPose = stateList[frame_number].BonePoses.TryGetValue(bone_id,out pose);
        return pose.Position;
    }
}
