using System.Collections;
using System.Collections.Generic;
using System;
using Unity;
using UnityEngine;
public class Evaluation 
{
    public List<int> GetKeyFrames(List<OptitrackSkeletonState> stateBuffer)
    {
        //List<int> a = new List<int>();
        //return a;
        return new List<int>() { 12354 };
        
    }
    
    //Statelist array with length (n) number of frames in the take
    //In each entry of statelist 

    public int GetHighestLeftElbow(List<OptitrackSkeletonState> stateList){
        float max_elbow = 0;
        int frame_number = 0;
        for (int i = 0 ; i < stateList.Count ; i++){
            // the line below takes the y coordinate of LFArm of every frame in the take 
            float comparison = stateList[i].BonePoses.values[7].Position.y;
            if (comparison > max_elbow){
                frame_number = i;
                max_elbow = comparison;
            }
            else{
                continue;
            }
        }
        //Debug.Log(stateList[0].BonePoses.values[0].Position.y.GetType());
        Debug.Log(stateList[0].BonePoses.values[8].Position.y);
        return frame_number;
    }

    public float GetJointAngle(List<OptitrackSkeletonState> stateList, int time_point, List<String> pivot_names){
        
        
    }

}
