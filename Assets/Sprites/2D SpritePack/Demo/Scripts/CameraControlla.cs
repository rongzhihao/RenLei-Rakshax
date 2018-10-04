using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlla : MonoBehaviour {
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	[SerializeField]
	private Transform target;
	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp(target.position.x,xMin,xMax), Mathf.Clamp(target.position.y,yMin,yMax), transform.position.z);
	}
}
