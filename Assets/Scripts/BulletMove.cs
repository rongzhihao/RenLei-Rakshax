using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    /*
     * Please put this weapon under tag "Damage2"
     * 
     */
    // Use this for initialization
    //public GameObject bullet;
    public float damage=2;
    private float cc = 1f;
    public int ff = 0;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        this.gameObject.SetActive(false);
    }
}
