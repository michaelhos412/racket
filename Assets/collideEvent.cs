using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collideEvent : MonoBehaviour
{
    // public GameObject gameObject2;
    void OnCollisionEnter(Collision collision) {
        // Debug.Log(gameObject2.name.ToString());
        // gameObject2.SetActive(false);
        gameObject.transform.localScale -= gameObject.transform.localScale; 
        StartCoroutine(Respawn(1f));
    }
 
    IEnumerator Respawn(float timeToRespawn) {
        yield return new WaitForSeconds(timeToRespawn);
        // gameObject2.SetActive(true);
        gameObject.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);
        gameObject.transform.position = new Vector3(0.148f, 3.29f, 2.75f);
    }
}