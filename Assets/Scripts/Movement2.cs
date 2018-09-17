using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Movement2 : MonoBehaviour {


    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float flySpeed = 5f;
    public SpriteRenderer sr; //sprite renderer in player
  //  public Animator anim;
    public LayerMask Ground;
    public Func myfunc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Defense.CanMove == true)
        {
            move();
            fly("w");
        }
        GameObject item = pickup("k");
        if (item != null)
        {
            Debug.Log(333);
            if (myfunc.isOnGround(item.transform,Ground))
            {
                Debug.Log(444);
                Destroy(item.GetComponent<Rigidbody2D>());
            }
        }



    }
    //public void myThread(Transform itemTransform)
    //{
    //    while (!myfunc.isOnGround(itemTransform,Ground))
    //    {

    //    }
    //    Destroy(itemTransform);
    //}
    //void afterDrop(GameObject item)
    //{

    //    item.AddComponent<Rigidbody2D>();
    //    //Thread newThread = new Thread(myThread);
    //    //newThread.Start(item.transform);
    //    Thread newThread = new Thread(() => myThread(item.transform));
    //    newThread.Start();



    //}

    void OnTriggerEnter2D()
    {
        //Destroy(transform.gameObject);
    }


    void move()
    {
        // set animation
        float move = Input.GetAxis("Horizontal");
       // anim.SetFloat("Speed", Mathf.Abs(move));

        //set movement
        if (move < 0)
        {
            sr.flipX = true;
        }
        else if (move > 0)
        {
            sr.flipX = false;
        }
        rb.velocity = new Vector2(move * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
    /*
     * param:(string) key for flying
     */ 
    void fly(string flyKey)
    {
        
        //set movement
        if (Input.GetKey(flyKey))
        {
            //set animation
          // anim.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0, flySpeed), ForceMode2D.Force);
        }
        //if (myfunc.isOnGround(transform, Ground))
        //{
        //    anim.SetBool("isJumping", false);
        //}
    }
    /*
     * param:(string) key for pickup
     */
    GameObject pickup(string pickupKey)
    {
        GameObject result = null;
        
        if (Input.GetKeyDown(pickupKey))
        {
            //search for all gameobjects with tag "Items"
            GameObject[] go = GameObject.FindGameObjectsWithTag("Items");
            
            if (!myfunc.ifItemsExist(transform)) // nothing in hand
            {
                for (int i = 0; i < go.Length; i++)
                {//loop all gameobjects with tag "Items"
                    //if one item's x position is near to character's x position, then pick it up
                    //it is decided by the gameobjects list(pick up whichever ranks higher in the list, not by distance)
                    if (Mathf.Abs(go[i].transform.position.x - transform.position.x) < 1 && Mathf.Abs(go[i].transform.position.y- transform.position.y)<1)
                    {
                        Debug.Log(go[i].name + " less than 1");
                        
                        
                        go[i].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                        go[i].transform.SetParent(transform);
                        break; // After pick up one, can not pick up another one(one bug: no distance included)
                    }
                }
            }
            else  // already something in hand
            {
                int flag = 0;// 1 means drop and pick up, 0 means drop only
                for (int i = 0; i < go.Length; i++)
                {//loop all gameobjects with tag "Items"
                    if (go[i].transform.parent != transform)
                    {//exclude the one already in hand

                        //if one item's x position is near to character's x position, then drop and pick up
                        if (System.Math.Abs(go[i].transform.position.x - transform.position.x)<1 && Mathf.Abs(go[i].transform.position.y - transform.position.y) < 1)
                        {
                            Transform item = myfunc.getItemChildTransform(transform);
                            
                            item.SetParent(null);//drop
                            //item.gameObject.AddComponent<Rigidbody2D>();
                            result = item.gameObject;
                            Debug.Log(go[i].name + " less than 1");
                            go[i].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                            go[i].transform.SetParent(transform);
                            flag = 1;
                            break; // After picking up one, can not pick up another one(one bug: no distance included)
                        }
                    }
                }
                if (flag == 0)// no one item's x position is near(exclude the one in hand), then only drops it.
                {
                    Transform item = myfunc.getItemChildTransform(transform);

                    item.SetParent(null);//drop
                    Debug.Log(123123);
                    //item.gameObject.AddComponent<Rigidbody2D>();
                    //item.gameObject.GetComponent<Rigidbody2D>().bodyType=Rigidbody2D.
                    result = item.gameObject;
                }

            }
        }
        return result;
    }

    
}
