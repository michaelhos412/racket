using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{    
    public GameObject popUpContainer;
    private ShowPopUp _script;
    public GameObject floatingText;
    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        _script = popUpContainer.GetComponent<ShowPopUp>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        showPopUp();
    }

        public void showPopUp()
    {
        // Instantiate(floatingText, new Vector3(4.515f, 2.501f, 1.787f), Quaternion.identity);
        Instantiate(floatingText, container.position, container.rotation);
        // PointsPopUp indicator = Instantiate(popUpText, transform.position, Quaternion.identity, transform).GetComponent<PointsPopUp>();
        // indicator.SetPointsText(100);
    }

}

