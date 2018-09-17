using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

    // Use this for initialization
    public GameObject shelter;
    public GameObject person;
    public static bool CanMove = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
          
            
            shelter.SetActive(true);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = shelter.transform.localPosition.z;
            shelter.transform.position = new Vector3(x, y, z);
            CanMove = false;
            Invoke("ShelterDisappear", 2f);
        }
    }
    void ShelterDisappear()
    {
        shelter.SetActive(false);
        CanMove = true;
    }

}
