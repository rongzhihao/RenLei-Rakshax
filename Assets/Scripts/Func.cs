using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func
{
    /*
     * param: Player's transform
     * return: If existing one item is the child of Player(like holding a weapon), return True
     */
    public bool ifItemsExist(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Items")
            {
                return true;
            }

        }
        return false;
    }
    /*
     * param: one transform
     * return: its child gameobject's transform with "Item" tag
     */
    public Transform getItemChildTransform(Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Items")
            {
                return transform.GetChild(i);
            }

        }
        return null;
    }

    public bool isOnGround(Transform t, LayerMask Ground)
    {
        RaycastHit2D ray = Physics2D.Raycast(t.position, new Vector2(0, -1), 1.0f, Ground);
        if (ray.collider != null)
        {
            return true;
        }
        return false;
    }

}

