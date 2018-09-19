using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photoHandler : MonoBehaviour {

	public GameObject connectedMenu;

	public void disableMenuUI(){
		PhotonNetwork.LoadLevel("PhotonGame");
	}
}
