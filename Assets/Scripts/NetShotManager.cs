using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetShotManager : MonoBehaviour
{
    [Header("Shuttlecock")]
    public GameObject shuttlecock = null;
    private ResetShuttlecock _shuttlecockScript = null;
    [Header("Racket")]
    public GameObject racket = null;
    private collideEvent _racketScript = null;
    // Start is called before the first frame update
    void Start()
    {
        _racketScript = racket.GetComponent<collideEvent>();
        _shuttlecockScript = shuttlecock.GetComponent<ResetShuttlecock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider obj)
    {
        if (_shuttlecockScript.gameMode == ResetShuttlecock.GameModes.NetShotDrill)
        {
            if(obj.transform.name == "Shuttlecock (1)")
            {
                _racketScript.scoreAmount += 100;
            }
        }
    }
}
