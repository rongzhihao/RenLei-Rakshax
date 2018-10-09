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
		if(PhotonNetwork.playerList.Length <= 1){
			SpawBullets();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void SpawBullets (){
        Vector3 bulletInitPlace1 = new Vector3(-1,1,0);
        GameObject bullet1 = (GameObject)PhotonNetwork.Instantiate(redBullet.name, bulletInitPlace1, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
	
		Vector3 bulletInitPlace2 = new Vector3(-3,1,0);
        GameObject bullet2 = (GameObject)PhotonNetwork.Instantiate(redBullet.name, bulletInitPlace2, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
	
	}
}
