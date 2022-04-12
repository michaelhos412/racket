using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text scoreAmount;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
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
        scoreAmount.text = string.Format("{0:0000}", score);
    }
    public void OnClickResetScore()
    {
        score = 0;
    }
}
