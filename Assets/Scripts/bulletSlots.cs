using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSlots : MonoBehaviour {

	
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform; 
	}
	
	// Update is called once per frame
	void LateUpdate () {
		keepSlotsAlwaysOnCamera();
	}


	private void keepSlotsAlwaysOnCamera()
    {
		Debug.Log(player.transform.position);
        //transform.position = player.transform.position + new Vector3(-6f,4.5f,0);
		transform.position = new Vector3(player.position.x - 6.2f,player.position.y + 4.5f,
							transform.position.z);
    }
}
