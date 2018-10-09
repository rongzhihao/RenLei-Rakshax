using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSlots : MonoBehaviour {

	private GameObject[] redSlotsArray;
	private GameObject[] blueSlotsArray;
	private GameObject randomSlot;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player"); 
		redSlotsArray = GameObject.FindGameObjectsWithTag("RedSlot");
		blueSlotsArray = GameObject.FindGameObjectsWithTag("BlueSlot");
		randomSlot = GameObject.FindGameObjectWithTag("RandomSlot");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		keepSlotsAlwaysOnCamera();
	}

	void Update (){
		checkBullet();
	}

    private void checkBullet()
    {
		int redBulletCount = player.GetComponent<PlayerController>().redBullet;
		int blueBulletCount = player.GetComponent<PlayerController>().blueBullet;
		Debug.Log("redBulletCount:"+redBulletCount);
		Debug.Log("blueBulletCount:"+blueBulletCount);
		if(redBulletCount < 3){
			reloadSlots(redBulletCount, Color.red, redSlotsArray);
		}else{
			reloadSlots(2, Color.red, redSlotsArray);
			randomSlot.GetComponent<SpriteRenderer>().color = Color.red;
		}

		if(blueBulletCount < 3){
			reloadSlots(blueBulletCount, Color.blue, blueSlotsArray);
		}else{
			reloadSlots(2, Color.blue, blueSlotsArray);
			randomSlot.GetComponent<SpriteRenderer>().color = Color.blue;
		}

		if( redBulletCount == 0 && randomSlot.GetComponent<SpriteRenderer>().color == Color.red){
			randomSlot.GetComponent<SpriteRenderer>().color = Color.white;
		}

		if( blueBulletCount == 0 && randomSlot.GetComponent<SpriteRenderer>().color == Color.blue){
			randomSlot.GetComponent<SpriteRenderer>().color = Color.white;
		}
    }

    private void keepSlotsAlwaysOnCamera()
    {
		transform.position = new Vector3(player.transform.position.x - 6.2f, player.transform.position.y + 4.5f, transform.position.z);
    }

	private void reloadSlots( int index, Color color,GameObject[] slots ){
		int slot = 0 ;
		for(; slot < index; slot++){
			slots[slot].GetComponent<SpriteRenderer>().color = color;
		}

		for(; slot < 2; slot++){
			slots[slot].GetComponent<SpriteRenderer>().color = Color.white;
		}
		
	}


}
