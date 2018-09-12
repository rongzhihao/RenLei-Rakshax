using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class fireBall : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D myRigidbody;
	private Vector2 direction;
	[SerializeField]
	private float speed = 20f;
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myRigidbody.velocity = direction * speed; 
	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	public void initialize(Vector2 direction)
	{
		this.direction = direction;
	}
}
