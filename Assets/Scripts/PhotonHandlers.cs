using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonHandlers : MonoBehaviour {

	public PhotonButton photonB;
   
	public GameObject mainPlayer;

	private void Awake(){
		
		DontDestroyOnLoad(this.transform);
		PhotonNetwork.sendRate = 280;
		PhotonNetwork.sendRateOnSerialize = 250;
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	public void createNewRoom(){
		PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions(){MaxPlayers = 4}, null);
	}

	public void joinOrCreateRoom(){
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 4;
		PhotonNetwork.JoinOrCreateRoom(photonB.joinRoomInput.text, roomOptions, TypedLobby.Default);
	}
	public void moveScene(){
    
		PhotonNetwork.LoadLevel("Level02");
    
      
    }

	private void OnJoinedRoom(){
		moveScene();
		Debug.Log("we are connected to the room");
	}

	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode){
		if(scene.name == "Level02"){
			spawnPlayer();
		}
	}

	private void spawnPlayer(){
		PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);

    }
    
}
