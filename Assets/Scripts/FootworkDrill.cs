using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FootworkDrill : MonoBehaviour
{
    public GameObject TimerCanvas;
    public GameObject ScoreCanvas;
    public GameObject CountdownCanvas;
    public GameObject courtBase;
    public ResetShuttlecock shuttlecock;
    public List<Vector3> positionList = new List<Vector3>();
    public int totalHits = 5;
    public float minY;
    public float maxY;
    private bool playerTouchedBase = false;
    private int hitCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        TimerCanvas.SetActive(true);
        ScoreCanvas.SetActive(true);
        CountdownCanvas.SetActive(true);
        courtBase.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void TouchBase(){
        playerTouchedBase = true;
        // disable the sphere in the center of court
        courtBase.SetActive(false);
        // place shuttle in random position of the 4 corners
        int index =  Random.Range(0,3);
        Vector3 pos = positionList[index]; 
       
        pos.y = Random.Range(minY, maxY); // randomize the height
        
        shuttlecock.placeShuttlecock(pos);
        
    }

    public void FootworkDrillShuttlecockCollideEvent(){
        hitCounter += 1;
        courtBase.SetActive(true);
    }
}
