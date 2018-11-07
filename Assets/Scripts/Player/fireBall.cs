using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class fireBall : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D myRigidbody;
	private Vector2 direction = Vector2.left;
	[SerializeField]
	private float speed = 10f;
	private int distance = 0;

	public float maxDistance = 100;
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		//InvokeRepeating("destroyFireBall", 0.05f, 0.05f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myRigidbody.velocity = direction * 10; 
		distance += 1;
		if(distance > maxDistance){
			PhotonNetwork.Destroy(gameObject);
		}
		
	}

	// void OnTriggerEnter2D(Collider2D other) {
	// 	PhotonNetwork.Destroy(gameObject);
	// }
	void OnBecameInvisible()
	{
		PhotonNetwork.Destroy(gameObject);
	}

	private void destroyFireBall () {
		PhotonNetwork.Destroy(gameObject);
	}
	public void initialize(Vector2 direction)
	{
		this.direction = direction;
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Bullet trigger");
        Debug.Log("photonView:" + this.GetComponent<PhotonView>().isMine);
        if (this.GetComponent<PhotonView>().isMine)
        {
           // PhotonNetwork.Destroy(this.GetComponent<PhotonView>().gameObject);

        }
    }
}
