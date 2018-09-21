using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camAndhpbarFollow : MonoBehaviour {
    public GameObject cam;
    public Image hpbar;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        cam.transform.position = this.transform.position+new Vector3(0,2,-2);
        hpbar.transform.position = this.transform.position + new Vector3(0, 1.5f, 0);

    }
}
