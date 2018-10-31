using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humancontrol : MonoBehaviour {

    // Use this for initialization
  
    private Animator _animator;




    void Start () {
        _animator = this.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("sss");
            _animator.SetFloat("speed", 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Debug.Log("s");
            _animator.SetFloat("speed", 0.0f);
        }
        if (Input.GetKey(KeyCode.X))
        {
            
            _animator.SetBool("jump", true);
        }
       
        if (Input.GetKey(KeyCode.C))
        {

            _animator.SetBool("attack", true);
        }
       
    }
}
