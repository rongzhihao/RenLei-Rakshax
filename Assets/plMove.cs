using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plMove : Photon.MonoBehaviour {

	public bool devTestng = false;
	public PhotonView photonView;
	public float moveSpeed = 100f;
	public float jumpForce = 800f;

	private Vector3 selfPos;

	public Text plName;
	public GameObject sceneCam;
	public GameObject plCam;



	// Use this for initialization
	void Start () {	
	}

	private void Awake(){
		if(!devTestng && photonView.isMine){
			//sceneCam = GameObject.Find("Main Camera");
			//sceneCam.SetActive(false);
			plCam.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!devTestng) {
			if(photonView.isMine)
				checkInput();
			else
				smoothNetMovement();
		}else{
			checkInput();
		}
	}

	private void checkInput(){
		var move = new Vector3(Input.GetAxis("Horizontal"), 0);
		transform.position += move * moveSpeed * Time.deltaTime;
	}

	private void smoothNetMovement(){
		transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
	}

	private void OnPhotoSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
		}else{
			selfPos = (Vector3)stream.ReceiveNext();
		}
	}
}
