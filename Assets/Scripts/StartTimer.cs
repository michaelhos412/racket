using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public Text timeText;
    public Text countdownText;
    public Text FinalScoreText;
    public Text Score;
    public GameObject shuttlecock;
    public GameObject playerHand;
    public GameObject UIHelper;
    public int timeToDisplay = 120;
    private int nextUpdate = 1;
    private int totalTime = -1;
    public GameObject EndEvaluationCanvas;

    [Header("Audio")]
    public AudioSource startWhistle = null;
    public AudioSource timesUp = null; 
    public AudioSource gamePlayBgm = null;
    [Header("Footwork Drill")]
    public GameObject footworkDrill = null;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayBgm = gamePlayBgm.GetComponent<AudioSource>();
        //get audio source for start whistle
        startWhistle = startWhistle.GetComponent<AudioSource>();
        timesUp = timesUp.GetComponent<AudioSource>(); 

        totalTime = timeToDisplay;
        // time.text = minute.ToString() + ":" + seconds.ToString();
    }

    void OnEnable()
    {
        totalTime = timeToDisplay;
    }

    void Update(){
        // If the next update is reached
        if(timeToDisplay > 0)
        {
            if(Time.time>=nextUpdate)
            {
            // Debug.Log(Time.time+">="+nextUpdate);
            // Change the next update (current second+1)
            nextUpdate=Mathf.FloorToInt(Time.time)+1;
            // Call your function
            UpdateEverySecond();
        }
        }
        else if(timeToDisplay == 0)
        {
            countdownText.text = "Time's Up!";
            // AudioListener.pause = true;
            shuttlecock.SetActive(false);
            EndEvaluationCanvas.SetActive(true);
            FinalScoreText.text = Score.text;
            playerHand.SetActive(true);
            UIHelper.SetActive(true);
            footworkDrill.SetActive(false);
            
        }
        if(timeToDisplay == totalTime - 2 )
        {
            countdownText.text = "";
        }
        if(timeToDisplay == 1)
        {
            timesUp.PlayDelayed(1);
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

    public void OnClickResetTimer()
    {
        // AudioListener.pause = true;
        // startWhistle.Play();
        countdownText.text = "Start!";
        // gamePlayBgm.PlayDelayed(1);
        timeToDisplay = 20;
    }

}
