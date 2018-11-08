using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSlots : MonoBehaviour {

	private GameObject[] slotsArray;
	private GameObject player;
	private string humanAnimator = "HumanIdle";
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player"); 
		slotsArray = GameObject.FindGameObjectsWithTag("Slots").OrderBy(g => g.transform.GetSiblingIndex()).ToArray();;
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
		//int redBulletCount = player.GetComponent<PlayerController>().redBullet;
		int blueBulletCount = player.GetComponent<PlayerController>().blueBullet;
		//UpdateSlots(0, redBulletCount, Color.red, slotsArray);
		UpdateSlots(0, blueBulletCount, Color.gray, slotsArray);
    }

    private void keepSlotsAlwaysOnCamera()
    {
		if(player.GetComponent<Animator>().runtimeAnimatorController.name.Equals(humanAnimator))
		{
			foreach( GameObject slot in slotsArray ){
				slot.SetActive(true);
			}
			transform.position = new Vector3(player.transform.position.x - 6.2f, player.transform.position.y + 4.5f, transform.position.z);
		}
		else
		{
			foreach( GameObject slot in slotsArray ){
				slot.SetActive(false);
			}
		}
		
    }

	private void UpdateSlots( int start, int end, Color color, GameObject[] slots ){
		int slot = start ;
		for(; slot < end; slot++){
			slots[slot].GetComponent<SpriteRenderer>().color = color;
		}
	
		for(; slot < slots.Length; slot++){
			slots[slot].GetComponent<SpriteRenderer>().color = Color.white;
		}
	}


}
