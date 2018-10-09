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
		DestroyItem();
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(other.tag);
		if(other.tag == "Player"){

			canBePickUp = true;
		}
	}

	void DestroyItem () {
		if(canBePickUp){
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
