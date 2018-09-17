using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour {

    // Use this for initialization
    public GameObject person;
    public GameObject bullet;
    public bool isBig = true;
    public bool isSmall = true;
    public bool Bigging = false;
    public bool Smalling = false;

	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B) && isBig && !Smalling) 
        {
            person.transform.localScale = new Vector3(2f, 2f, 2f);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            person.transform.position = new Vector3(x, y + 2, z);
            bullet.transform.localScale = new Vector3(2f, 2f, 2f);
            //isBig = false;
            Bigging = true;
            Invoke("BigEnd", 3f);

        }
        if (Input.GetKeyDown(KeyCode.V) && isSmall && !Bigging)
        {
            person.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            person.transform.position = new Vector3(x, y -1, z);
            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // isSmall = false;
            Smalling = true;
            Invoke("SmallEnd", 3f);
        }
      
    }
    void BigEnd()
    {
        person.transform.localScale = new Vector3(1f, 1f, 1f);
        float x = person.transform.localPosition.x;
        float y = person.transform.localPosition.y;
        float z = person.transform.localPosition.z;
        person.transform.position = new Vector3(x, y - 2, z);
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        Bigging = false;
    }
    void SmallEnd()
    {
        person.transform.localScale = new Vector3(1f, 1f, 1f);
        float x = person.transform.localPosition.x;
        float y = person.transform.localPosition.y;
        float z = person.transform.localPosition.z;
        person.transform.position = new Vector3(x, y +1, z);
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        Smalling = false;
    }
}
