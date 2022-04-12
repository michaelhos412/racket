using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text scoreAmount;
    public int score = 0;

    public AudioSource earnPoint;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        earnPoint = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        score += 100;
        earnPoint.Play();
        scoreAmount.text = string.Format("{0:0000}", score);
    }
    public void OnClickResetScore()
    {
        score = 0;
    }
}
