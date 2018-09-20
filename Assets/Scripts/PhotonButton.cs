using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButton : MonoBehaviour {

	public PhotonHandlers pHnadler;
	public InputField createRoomInput, joinRoomInput, nameInput;

	public void onClickCreateRoom(){
		pHnadler.createNewRoom();
	}

	public void onClickJoinRoom(){
		pHnadler.joinOrCreateRoom();
	}
}
