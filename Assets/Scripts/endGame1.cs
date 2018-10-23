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
         if(PhotonNetwork.connectionState == ConnectionState.Connected){
             PhotonNetwork.LoadLevel("gameConnect");
         }
    }

    void OnMouseUp()
    {
        PhotonNetwork.Disconnect();
       
       
        
    }
}
