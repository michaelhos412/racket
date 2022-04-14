using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationtest : MonoBehaviour
{
    Animator anim;
    Animation _anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _anim = gameObject.GetComponent<Animation>();
        anim.speed = 1;
        anim.Play("Base Layer.Backhand_receive2_success_001_FINAL", 0,0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
