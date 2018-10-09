using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

	private bool canBePickUp =false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(other.tag);
		if(other.tag == "Player"){
			if(gameObject.tag == "RedSlot"){
				other.gameObject.GetComponent<PlayerController>().AddBullet(0);
			}else{
				other.gameObject.GetComponent<PlayerController>().AddBullet(1);
			}
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
