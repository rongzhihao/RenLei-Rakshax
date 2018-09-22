using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2 : MonoBehaviour {
    public GameObject person;
    public GameObject originalBullet;
    public float bulletSpeed=15;
    
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float x = person.transform.position.x;
            float y = person.transform.position.y;
            float z = person.transform.position.z;
            GameObject bulletCopy = Instantiate(originalBullet, new Vector3(x, y, z), Quaternion.identity,this.transform.parent.gameObject.transform);
            Debug.Log(x + " " + y + " " + z);
            bulletCopy.SetActive(true);
            bulletCopy.AddComponent<Rigidbody2D>();
            bulletCopy.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (person.GetComponent<SpriteRenderer>().flipX)
            {
                bulletCopy.transform.position= new Vector3(x-2, y, z);
                bulletCopy.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
                Debug.Log(bulletCopy.GetComponent<Rigidbody2D>().velocity);
                bulletCopy.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                bulletCopy.transform.position = new Vector3(x + 2, y, z);
                bulletCopy.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
                Debug.Log(bulletCopy.GetComponent<Rigidbody2D>().velocity);
                bulletCopy.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            
        }

        if (shoot.hp <= 0)
        {
            //person.GetComponent<SpriteRenderer>().sprite = Resources.Load("image/zombie", typeof(Sprite)) as Sprite;
            person.GetComponent<SpriteRenderer>().color = Color.green;
        }      

    }
    void destroy(GameObject gob)
    {
        Destroy(gob);
    }
}
