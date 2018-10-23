using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame1 : MonoBehaviour {

    // Use this for initialization
    public GameObject button;
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		checkPhotonStatic();
	}

    private void checkPhotonStatic()
    {
        Debug.Log("conn:"+ (PhotonNetwork.connectionState == ConnectionState.Disconnected));
         if(PhotonNetwork.connectionState == ConnectionState.Disconnected){
             PhotonNetwork.LoadLevel("gameConnect");
         }
    }

    void OnMouseUp()
    {
        PhotonNetwork.DestroyAll();
        PhotonNetwork.Disconnect();
    }
}
