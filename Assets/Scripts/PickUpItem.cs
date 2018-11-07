using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

	private bool canBePickUp =false;
	private string humanAnimator = "HumanIdle";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Equals(humanAnimator));
		if(other.tag == "Player" && other.gameObject.GetComponent<Animator>().runtimeAnimatorController.name.Equals(humanAnimator)){
			if(gameObject.tag == "RedSlot"){
				other.gameObject.GetComponent<PlayerController>().AddBullet(0);
			}else{
				other.gameObject.GetComponent<PlayerController>().AddBullet(1);
			}
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
