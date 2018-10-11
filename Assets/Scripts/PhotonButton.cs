using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButton : MonoBehaviour {

	public PhotonHandlers pHnadler;
	public InputField createRoomInput, joinRoomInput, nameInput;



    public void onClickCreateRoom(){
        PhotonNetwork.playerName = nameInput.text;
        pHnadler.createNewRoom();
	}

	public void onClickJoinRoom(){
        PhotonNetwork.playerName = nameInput.text;
        pHnadler.joinOrCreateRoom();
	}
}
