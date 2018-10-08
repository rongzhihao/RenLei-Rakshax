using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {
    public string pickUpKey = "k";
    public GameObject slotsPrefab;

    public static int[] blueBag = new int[2]; // white = 0, blue = 1
    private static int[] redBag = new int[2];// white = 0, red = 1
    private static int[] theOtherBag = new int[1];// blue = 1, red = 2
    private GameObject slots;
    // Use this for initialization
    void Start()
    {
        initBags();//set all spots in bags = 0 (means every spot has no item)

        Vector3 playerPos = transform.position;
        slots = Instantiate(slotsPrefab, playerPos, slotsPrefab.transform.rotation);

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
    private void keepSlotsAlwaysOnCamera()
    {
        slots.transform.position = transform.position + new Vector3(-2.25f,2,0);
    }
    // Update is called once per frame
    void Update()
    {
        keepSlotsAlwaysOnCamera();
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
        

        syncBagsToImage();
    }

    void syncBagsToImage()
    {
        //Debug.Log("enter syncBagsToImage");
        //Debug.Log(GameObject.Find("BlueSlot1").GetComponent<SpriteRenderer>().color);
        for (int i = 0; i < blueBag.Length; i++)
        {
            string name = "BlueSlot" + (i+1);
            if (blueBag[i] == 1)
            {
                slots.transform.Find(name).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                slots.transform.Find(name).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        for (int i = 0; i < redBag.Length; i++)
        {
            string name = "RedSlot" + (i + 1);
            if (redBag[i] == 1)
            {
                slots.transform.Find(name).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                slots.transform.Find(name).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (theOtherBag[0] == 1)
        {
            slots.transform.Find("RandomSlot").gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(theOtherBag[0] == 2)
        {
            slots.transform.Find("RandomSlot").gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            slots.transform.Find("RandomSlot").gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public int[] getNumberOfBullet()
    {
        int[] result = new int[] {0,0,0,0,0};
        result[0] = blueBag[0];
        result[1] = blueBag[1];
        result[2] = redBag[0];
        result[3] = redBag[1];
        result[4] = theOtherBag[0];

        return result;

    }
    /*
     * After shooting a blue bullet, please call this function to change bullet status in slots
     */

    public void shootBlueBullet()
    {
        if(blueBag[0]==0)
        {
            if (blueBag[1] == 0)
            {
                if (theOtherBag[0] == 1)
                {
                    theOtherBag[0] = 0;
                }
                else
                {
                    // no blue bullet
                }
            }
            else
            {
                blueBag[1] = 0;
            }
        }
        else
        {
            blueBag[0] = 0;
        }
        
    }
    /*
     * After shooting a red bullet, please call this function to change bullet status in slots
     */
    public void shootRedBullet()
    {
        if (redBag[0] == 0)
        {
            if (redBag[1] == 0)
            {
                if (theOtherBag[0] == 2)
                {
                    theOtherBag[0] = 0;
                }
                else
                {
                    // no red bullet
                }
            }
            else
            {
                redBag[1] = 0;
            }
        }
        else
        {
            redBag[0] = 0;
        }
    }
}
