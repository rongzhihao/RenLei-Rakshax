using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuLogic : MonoBehaviour {

	public GameObject connectedMenu;

	public void disableMenuUI(){
		PhotonNetwork.LoadLevel("PhotonGame");
	}
}
