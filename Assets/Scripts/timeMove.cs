using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float movespeed = PlayerController.Instance.moveSpeed;
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * movespeed * Time.deltaTime, Space.World);
    }
}
