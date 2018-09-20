using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

    // Use this for initialization
    public GameObject shelter;
    public GameObject person;
    public static bool CanMove = true;
    public string defenseKey = "z";
    public float shelterTime = 2f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(defenseKey))
        {
          
            
            shelter.SetActive(true);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            shelter.transform.localPosition = new Vector3(x, y, z);
            CanMove = false;
            Invoke("ShelterDisappear", shelterTime);
        }
    }
    void ShelterDisappear()
    {
        shelter.SetActive(false);
        CanMove = true;
    }

}
