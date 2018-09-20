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
    public string bigKey = "b";
    public string smallKey = "v";
    public float bigOrSmallTime = 3f;
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(bigKey) && isBig && !Smalling) 
        {
            person.transform.localScale = new Vector3(2f, 2f, 2f);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            person.transform.localPosition = new Vector3(x, y + 2, z);
            bullet.transform.localScale = new Vector3(2f, 2f, 2f);
            //isBig = false;
            Bigging = true;
            Invoke("BigEnd", bigOrSmallTime);

        }
        if (Input.GetKeyDown(smallKey) && isSmall && !Bigging)
        {
            person.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            person.transform.localPosition = new Vector3(x, y -1, z);
            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // isSmall = false;
            Smalling = true;
            Invoke("SmallEnd", bigOrSmallTime);
        }
      
    }
    void BigEnd()
    {
        person.transform.localScale = new Vector3(1f, 1f, 1f);
        float x = person.transform.localPosition.x;
        float y = person.transform.localPosition.y;
        float z = person.transform.localPosition.z;
        person.transform.localPosition = new Vector3(x, y - 2, z);
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        Bigging = false;
    }
    void SmallEnd()
    {
        person.transform.localScale = new Vector3(1f, 1f, 1f);
        float x = person.transform.localPosition.x;
        float y = person.transform.localPosition.y;
        float z = person.transform.localPosition.z;
        person.transform.localPosition = new Vector3(x, y +1, z);
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        Smalling = false;
    }
}
