using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{    
    public GameObject floatingText = null;
    private ResetShuttlecock _shuttlecockScript = null;

    // Start is called before the first frame update
    void Start()
    {
        _shuttlecockScript = gameObject.GetComponent<ResetShuttlecock>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_shuttlecockScript.gameMode != ResetShuttlecock.GameModes.NetShotDrill)
        {
            if (collision.gameObject.tag == "Player")
            {
                showPopUp();
            }
        }
    }

    public void OnTriggerExit(Collider obj)
    {
        if (_shuttlecockScript.gameMode == ResetShuttlecock.GameModes.NetShotDrill)
        {
            if(obj.transform.name == "NetShotCollider")
            {
                showPopUp();
            }
        }
    }

    public void showPopUp()
    {
        Instantiate(floatingText, gameObject.transform.localPosition, Quaternion.identity);
        // PointsPopUp indicator = Instantiate(popUpText, transform.position, Quaternion.identity, transform).GetComponent<PointsPopUp>();
    }

}

