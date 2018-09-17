using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inBullet : MonoBehaviour {

    public GameObject bullet;
    public GameObject hp1, hp2;
    void Start()
    {
        Invoke("BulletPlace", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * 5 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // GameObject.Destroy(this.gameObject);
        bullet.SetActive(false);
        Debug.Log("Defense successfully!!!");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // GameObject.Destroy(this.gameObject);
        bullet.SetActive(false);
        shoot.hp--;
        if (shoot.hp == 1)
            hp2.SetActive(false);
        if (shoot.hp == 0)
            hp1.SetActive(false);
    }
    void BulletPlace()
    {
        bullet.transform.position = new Vector3(-0.31f,3.35f, 0);
        bullet.SetActive(true);
        Invoke("BulletPlace", 3f);
    }
}
