using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    // Use this for initialization
    public GameObject bullet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (shoot.left)
        //{
        //    transform.Translate(Vector3.left * 5 * Time.deltaTime, Space.World);
        //}
        //else
        //{
        //    transform.Translate(Vector3.right * 5 * Time.deltaTime, Space.World);
        //}
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        bullet.SetActive(false);
    }
}
