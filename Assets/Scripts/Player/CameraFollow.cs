using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	private Transform target;
	[SerializeField]
	private float maxX;

	[SerializeField]
	private float maxY;

	[SerializeField]
	private float minX;

	[SerializeField]
	private float minY;

	void Start () {
		target = GameObject.Find("zombie").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), 
								Mathf.Clamp(target.position.y, minY, maxY),
									transform.position.z);
	}
}
