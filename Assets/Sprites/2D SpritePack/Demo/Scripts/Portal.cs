using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public GameObject _portal;
	void Update () {
		_portal.transform.Rotate (new Vector3 (0f, 0f, 3f));
	}
}
