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
		
	}
    void OnMouseUp()
    {
        Debug.Log("aa");
        PhotonNetwork.LoadLevel("gameConnect");
    }
}
