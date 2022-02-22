using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public Text timeText;
    public int timeToDisplay = 120;
    private int nextUpdate = 1;
    // Start is called before the first frame update
    void Start()
    {
        // timer.text = minute.ToString() + ":" + seconds.ToString();
    }

    void Update(){
     
        // If the next update is reached
        if(Time.time>=nextUpdate){
            Debug.Log(Time.time+">="+nextUpdate);
            // Change the next update (current second+1)
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            // Call your fonction
            UpdateEverySecond();
        }
     
    }

    // Update is called once per frame
    void UpdateEverySecond()
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
