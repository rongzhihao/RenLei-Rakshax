using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hurt : MonoBehaviour {
    /*
     * Please make sure "isTrigger" in "Box Collider" is checked for all the bullets
     * Do not check "isTrigger" for players
     */
    public float fullHealthValue = 10;
    public Image HealthBar_Front;
    private float currentHealthValue;
    public bool showHealthBar = true;

    void Start () {
        if (showHealthBar)
        {
            HealthBar_Front.transform.parent.gameObject.SetActive(true);
        }
        currentHealthValue = fullHealthValue;
    }
	
	void Update () {
        if (currentHealthValue <= 0)
        {
            //died
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damage2")
        {
            //hurt by bullet
            currentHealthValue-=2;
            HealthBar_Front.fillAmount = currentHealthValue/fullHealthValue;
        }
    }
}
