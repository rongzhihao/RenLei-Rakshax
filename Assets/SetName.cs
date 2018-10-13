using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour {
    public PhotonView p;
    // Use this for initialization
    void Start () {
        if (p.owner.name == "")
            p.owner.name = "Player"+PhotonNetwork.player.ID.ToString();
        GetComponent<Text>().text = p.owner.name;
      
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
