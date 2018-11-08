using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawBullet2 : MonoBehaviour {

    [SerializeField]
    private GameObject[] bulletPlaces;

    [SerializeField]
    private int second = 5;

    [SerializeField]
    private GameObject bullet;

    private int milliSecond;
    DateTime dateTime;

    // Use this for initialization
    void Start () {
        dateTime = DateTime.Now;
        milliSecond = second * 1000;
    }
	
	// Update is called once per frame
	void Update () {
        if ((DateTime.Now - dateTime).TotalMilliseconds > milliSecond)
        {
            Debug.Log("5秒了");
            SpawBullets();
            dateTime = DateTime.Now;
        }
    }
    private void SpawBullets()
    {
        foreach (GameObject place in bulletPlaces)
        {
            SpawSingleBullet(place.transform, bullet);          
        }
    }

    private void SpawSingleBullet(Transform transform, GameObject BulletType)
    {
        Vector3 bulletInitPlace = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        GameObject bullet1 = (GameObject)PhotonNetwork.Instantiate(BulletType.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
    }

}
