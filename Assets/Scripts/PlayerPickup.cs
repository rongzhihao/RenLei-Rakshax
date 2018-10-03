using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {
    public string pickUpKey = "k";
    private int[] blueBag = new int[2]; // white = 0, blue = 1
    private int[] redBag = new int[2];// white = 0, red = 1
    private int[] theOtherBag = new int[1];// blue = 1, red = 2

    // Use this for initialization
    void Start()
    {
        initBags();//set all spots in bags = 0 (means every spot has no item)

    }
    //inital bags
    void initBags()
    {
        for (int i = 0; i < blueBag.Length; i++)
        {
            blueBag[i] = 0;
            redBag[i] = 0;
        }
        theOtherBag[0] = 0;
    }
    public bool ifArrayIsFull(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                return false;
            }
        }
        return true;
    }
    public bool setFirstEmptySlotPositionToValueOne(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                array[i] = 1;
                return true;
            }
        }
        return false;
    }
    public bool setFirstEmptySlotPositionToValueTwo(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                array[i] = 2;
                return true;
            }
        }
        return false;
    }
    public void printArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            Debug.Log("111");
            //search for all gameobjects with tag "Items"
            GameObject[] go = GameObject.FindGameObjectsWithTag("Items");
            for (int i = 0; i < go.Length; i++)
            {//loop all gameobjects with tag "Items"
             //if one item's x position is near to character's x position, then pick it up
             //it is decided by the gameobjects list(pick up whichever ranks higher in the list, not by distance)
                if (Mathf.Abs(go[i].transform.position.x - transform.position.x) < 1 && Mathf.Abs(go[i].transform.position.y - transform.position.y) < 1)
                {
                    Debug.Log(go[i].name + " less than 1");

                    if (go[i].name == "BlueBullet")
                    {
                        if (ifArrayIsFull(blueBag))
                        {
                            if (theOtherBag[0] == 0)
                            {
                                bool firstEmptySlotPos = setFirstEmptySlotPositionToValueOne(theOtherBag);//blue
                                if (firstEmptySlotPos == false)
                                {
                                    Debug.Log("Error!");
                                }
                            }
                            else
                            {
                                Debug.Log("All slots are full for 'Blue' bullet");
                            }
                        }
                        else
                        {
                            bool firstEmptySlotPos = setFirstEmptySlotPositionToValueOne(blueBag);
                            if (firstEmptySlotPos == false)
                            {
                                Debug.Log("Error!");
                            }
                        }
                    }
                    else
                    {
                        if (ifArrayIsFull(redBag))
                        {
                            if (theOtherBag[0] == 0)
                            {
                                bool firstEmptySlotPos = setFirstEmptySlotPositionToValueTwo(theOtherBag);//red
                                if (firstEmptySlotPos == false)
                                {
                                    Debug.Log("Error!");
                                }
                            }
                            else
                            {
                                Debug.Log("All slots are full for 'Red' bullet");
                            }
                        }
                        else
                        {
                            bool firstEmptySlotPos = setFirstEmptySlotPositionToValueOne(redBag);
                            if (firstEmptySlotPos == false)
                            {
                                Debug.Log("Error!");
                            }
                        }
                    }

                    Destroy(go[i]);
                    break; // pick up only one item at one time.
                }
            }
        }
        if (Input.GetKeyDown("n"))
        {
            Debug.Log("blue:");
            printArray(blueBag);
            Debug.Log("red:");
            printArray(redBag);
            Debug.Log("other:");
            printArray(theOtherBag);
        }

        syncBagsToImage();
    }

    void syncBagsToImage()
    {
        for(int i = 0; i < blueBag.Length; i++)
        {
            string name = "BlueSlot" + (i+1);
            if (blueBag[i] == 1)
            {
                GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        for (int i = 0; i < redBag.Length; i++)
        {
            string name = "RedSlot" + (i + 1);
            if (redBag[i] == 1)
            {
                GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (theOtherBag[0] == 1)
        {
            GameObject.Find("RandomSlot").GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(theOtherBag[0] == 2)
        {
            GameObject.Find("RandomSlot").GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GameObject.Find("RandomSlot").GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
