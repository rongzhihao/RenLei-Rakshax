using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    // Use this for initialization
    public GameObject person;
    public GameObject bullet;
    public static bool left = false;
    public static bool left2 = false;
    public static int hp = 2;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            float x = person.transform.localPosition.x;
            float y = person.transform.localPosition.y;
            float z = person.transform.localPosition.z;
            if (person.GetComponent<SpriteRenderer>().flipX)
            {
                left = true;
                bullet.transform.position = new Vector3(x - 2, y, z);
                bullet.SetActive(true);
                bullet.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                left = false;
                bullet.transform.position = new Vector3(x + 2, y, z);
                bullet.SetActive(true);
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }
           
        }

        if (shoot.hp <= 0)
            //person.GetComponent<SpriteRenderer>().sprite = Resources.Load("image/zombie", typeof(Sprite)) as Sprite;
            person.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
