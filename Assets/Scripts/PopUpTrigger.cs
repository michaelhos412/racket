using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{    
    public GameObject floatingText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            showPopUp();
        }
    }

        public void showPopUp()
    {
        // Instantiate(floatingText, new Vector3(4.515f, 2.501f, 1.787f), Quaternion.identity);
        Instantiate(floatingText, gameObject.transform.localPosition, Quaternion.identity);
        // PointsPopUp indicator = Instantiate(popUpText, transform.position, Quaternion.identity, transform).GetComponent<PointsPopUp>();
        // indicator.SetPointsText(100);
    }

}

