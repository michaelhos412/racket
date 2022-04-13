using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToDestroy = null;
 
     // Use this for initialization
     void Start () {
         Destroy (ObjectToDestroy, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
     }
}
