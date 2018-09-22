using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Defense : MonoBehaviour {

    // Use this for initialization
    public GameObject shelter;
    public GameObject person;
    public static bool CanMove = true;
    public string defenseKey = "z";
    public float shelterTime = 2f;
    public Image ShelterBar_Front;
    float time;
    private Func myfunc;
    //private GameObject shelter;


    void Start () {
        myfunc = new Func();
    }
	
	// Update is called once per frame
	void Update () {
        if (shelter.activeSelf == false)
        {
            if (Input.GetKeyDown(defenseKey))
            {
                time = shelterTime;
                ShelterBar_Front.transform.parent.gameObject.SetActive(true);
                shelterBarController();
                float x = person.transform.localPosition.x;
                float y = person.transform.localPosition.y;
                float z = person.transform.localPosition.z;
                //shelter = Instantiate(originalShelter, new Vector3(x, y, z), Quaternion.identity, this.transform.parent.gameObject.transform);
                shelter.SetActive(true);
                shelter.transform.localPosition = new Vector3(x, y, z);
                CanMove = false;
                Invoke("ShelterDisappear", shelterTime);
            }
        }
        else
        {
            shelterBarController();
        }
    }
    void shelterBarController()
    {
        //Debug.Log(time);
        if (time > 0)
        {
            time -= Time.deltaTime;
            ShelterBar_Front.fillAmount = time / shelterTime;
        }
    }
    void ShelterDisappear()
    {
        shelter.SetActive(false);
        ShelterBar_Front.transform.parent.gameObject.SetActive(false);
        CanMove = true;
    }

}
