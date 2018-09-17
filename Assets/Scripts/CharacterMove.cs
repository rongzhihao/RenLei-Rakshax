using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    // Use this for initialization
    public GameObject rb;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Defense.CanMove)
        {
            float movespeed = 3.0f;
            float h = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector3.right * h * movespeed * Time.deltaTime, Space.World);
        }
       
    }
}
