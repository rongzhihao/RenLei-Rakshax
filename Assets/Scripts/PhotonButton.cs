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
        ExitGames.Client.Photon.Hashtable abc = new ExitGames.Client.Photon.Hashtable();
        abc["name"] = nameInput.text;
        PhotonNetwork.player.SetCustomProperties(abc);
    }

	public void onClickJoinRoom(){
        PhotonNetwork.playerName = nameInput.text;
        pHnadler.joinOrCreateRoom();
        ExitGames.Client.Photon.Hashtable abc = new ExitGames.Client.Photon.Hashtable();
        abc["name"] = nameInput.text;
        PhotonNetwork.player.SetCustomProperties(abc);
    }
}
