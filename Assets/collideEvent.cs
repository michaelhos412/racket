using UnityEngine;
using System.Collections;

public class collideEvent : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(Respawn(1f));
    }
 
    IEnumerator Respawn(float timeToRespawn) {
        yield return new WaitForSeconds(timeToRespawn);
        gameObject.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);
        gameObject.transform.localPosition = new Vector3(0.148f, 4.12f, 3.4f);
    }
}