using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collideEvent : MonoBehaviour
{
    int counter = 0;
    // List<Vector3> pos = new List<Vector3>(5);
    Vector3[] pos = {new Vector3(1.555f, 3.26f, 2.82f), new Vector3(-0.414f, 3.783f, 3.219f), new Vector3(-1.297f, 3.52f, 2.58f), new Vector3(-0.544f, 2.482f, 2.984f), new Vector3(-0.423f, 4.021f, 3.525f)};
    // List<Vector3> posList = new List<Vector3>(pos);
    void OnCollisionEnter(Collision collision) {
        // Debug.Log(gameObject2.name.ToString());
        // gameObject2.SetActive(false);
        gameObject.transform.localScale -= gameObject.transform.localScale; 
        StartCoroutine(Respawn(1f));
    }
 
    IEnumerator Respawn(float timeToRespawn) {
        counter += 1;
        yield return new WaitForSeconds(timeToRespawn);
        gameObject.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
        gameObject.transform.position = pos[counter%5];
    }
}