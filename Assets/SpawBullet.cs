using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawBullet : MonoBehaviour {

	[SerializeField]
	private GameObject redBullet;
	[SerializeField]
	private GameObject blueBullet;
	[SerializeField]
	private GameObject[] bulletPlaces;
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
		int count = bulletPlaces.Length;
		foreach(GameObject place in  bulletPlaces){
			if(count % 2 == 0){
			//	SpawSingleBullet(place.transform, redBullet);
			}else{
				SpawSingleBullet(place.transform, blueBullet);
			}
			count--;
		}
	}

	private void SpawSingleBullet ( Transform transform, GameObject BulletType){
		Vector3 bulletInitPlace = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
        GameObject bullet1 = (GameObject)PhotonNetwork.Instantiate(BulletType.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
	}
}
