using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawBullet : MonoBehaviour {

	[SerializeField]
	private GameObject redBullet;
	[SerializeField]
	private GameObject blueBullet;
	// Use this for initialization
	void Start () {
		SpawBullets();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void SpawBullets (){
        Vector3 bulletInitPlace = new Vector3(-1,1,0);
        GameObject bullet = (GameObject)PhotonNetwork.Instantiate(redBullet.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
	}
}
