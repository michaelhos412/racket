using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{
    public GameObject floatingText;
    public void show()
    {
        Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        // PointsPopUp indicator = Instantiate(popUpText, transform.position, Quaternion.identity, transform).GetComponent<PointsPopUp>();
        // indicator.SetPointsText(100);
    }
}
