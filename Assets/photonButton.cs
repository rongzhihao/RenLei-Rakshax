using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photonButton : MonoBehaviour {

	public photonHandlers pHnadler;
	public InputField createRoomInput, joinRoomInput;

	public void onClickCreateRoom(){
		pHnadler.createNewRoom();
	}

	public void onClickJoinRoom(){
		pHnadler.joinOrCreateRoom();
	}


}
