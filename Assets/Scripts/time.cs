using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour {

    // Use this for initialization
    public GameObject timeText;
    private float spendTime;
    private int hour, minute, second;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spendTime += Time.deltaTime;
        hour = (int)spendTime / 3600;
        minute = (int)(spendTime - hour * 3600) / 60;
        second = (int)(spendTime - hour * 3600 - minute * 60);
        timeText.GetComponent<TextMesh>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
    }
}
