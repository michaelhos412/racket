using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text;

public class SkeletonSaveLoadManager
{
    [Serializable]
    public class SkeletonStatesBuffer
    {
        public List<OptitrackSkeletonState> stateList; 
        public SkeletonStatesBuffer()
        {
            stateList = new List<OptitrackSkeletonState>();
        }

        public void Clear()
        {
            stateList.Clear();
        }
        public int Count()
        {
            return stateList.Count;
        }
    }

    public SkeletonStatesBuffer skeletonStateBuffer;

#if ANDROID
    private string savePath = Application.persistentDataPath + "/save.json";
#endif
    public SkeletonSaveLoadManager()
    {
        skeletonStateBuffer = new SkeletonStatesBuffer();
    }
    public void SaveSkeletonStatesBuffer(string savePath)
    {
        string json = JsonUtility.ToJson(skeletonStateBuffer, false);
        File.WriteAllText(savePath, json);
        Debug.Log("Saving " + skeletonStateBuffer.Count() + "skeletonState to" + savePath);
        Debug.Log("SkeletonStatesBuffer Json length: " + json.Length);
    }

    public void LoadSkeletonStatesBuffer(string savePath)
    {
        Debug.Log("Reading skeletonstatesbuffer json file at: " + savePath);
        this.skeletonStateBuffer = JsonUtility.FromJson<SkeletonStatesBuffer>(File.ReadAllText(savePath));
        Debug.Log("skeletonStateBuffer of length" + skeletonStateBuffer.Count() + " loaded!");
    }

    public void LoadSkeletonStatesBufferJson(string json)
    {
        Debug.Log("Reading skeletonstatesbuffer json file");
        this.skeletonStateBuffer = JsonUtility.FromJson<SkeletonStatesBuffer>(json);
        Debug.Log("skeletonStateBuffer of length" + skeletonStateBuffer.Count() + " loaded!");
    }


    public OptitrackSkeletonDefinition LoadSkeletonDefinition(string savePath)
    {
        Debug.Log("Reading json file at: " + savePath);
        OptitrackSkeletonDefinition def = JsonUtility.FromJson<OptitrackSkeletonDefinition>( File.ReadAllText( savePath ) );
        Debug.Log(def);
        Debug.Log("SkeletonDefinition json loaded!");
        return def;
    }

    public OptitrackSkeletonDefinition LoadSkeletonDefinitionAndroid(string savePath)
    {
        UnityWebRequest www = UnityWebRequest.Get(savePath);

        www.SendWebRequest();
        while (!www.downloadHandler.isDone)
        {

        }

        OptitrackSkeletonDefinition def = JsonUtility.FromJson<OptitrackSkeletonDefinition>(www.downloadHandler.text);
        Debug.Log("definition loaded from android");

        return def;
    }

    public void LoadSkeletonStateJsonAndroid(string savePath)
    {
        UnityWebRequest www = UnityWebRequest.Get(savePath);

        www.SendWebRequest();
        while (!www.downloadHandler.isDone)
        {

        }

        this.skeletonStateBuffer = JsonUtility.FromJson<SkeletonStatesBuffer>(www.downloadHandler.text);
        Debug.Log("state loaded from android of lenght" + this.skeletonStateBuffer.Count());
    }
    public void SaveSkeletonDefinition(OptitrackSkeletonDefinition def, string savePath)
    {
        string json = JsonUtility.ToJson(def);
        File.WriteAllText(savePath, json);
        Debug.Log("SkeletonDefinition Json length: " + json.Length);
    }

    public void SaveStreamingClientTransform(Transform transform, string savePath)
    {
        string json = JsonUtility.ToJson(transform, false);
        File.WriteAllText(savePath, json);
        Debug.Log("Saving " + "streaming transform to" + savePath);
        Debug.Log("Streaming transform Json length: " + json.Length);
    }

    public Transform LoadStreamingClientTransform(string savePath)
    {
        Debug.Log("Reading skeletonstatesbuffer json file at: " + savePath);
        Transform transform = JsonUtility.FromJson<Transform>(File.ReadAllText(savePath));
        Debug.Log("Transform loaded!" + transform );
        return transform;
    }
}
