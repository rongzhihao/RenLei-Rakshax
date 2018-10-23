using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviour {

	public string versionName = "0.1";
	[SerializeField]
	private GameObject sectionView1, sectionView2, sectionView3;

	private void Awake(){
		if(!(PhotonNetwork.connectionState == ConnectionState.Connected)){
			PhotonNetwork.ConnectUsingSettings(versionName);
		}else{
			OnConnectedToMaster();
		}
		Debug.Log("connecting to photon ...");
	}

	private void OnConnectedToMaster(){

		PhotonNetwork.JoinLobby(TypedLobby.Default);

		Debug.Log("we are connected to master");
     
	}

	private void OnJoinedLobby(){

		sectionView1.SetActive(false);

		sectionView2.SetActive(true);

		Debug.Log("On Joined Lobby");
	}

	private void OnDisconnectedFromPhoton(){
		if(sectionView1.active){
			sectionView1.SetActive(false);
		}

		if(sectionView2.active){
			sectionView2.SetActive(false);
		}

		sectionView3.SetActive(true);
		
		Debug.Log("disconnected from photon services");
	}
}
